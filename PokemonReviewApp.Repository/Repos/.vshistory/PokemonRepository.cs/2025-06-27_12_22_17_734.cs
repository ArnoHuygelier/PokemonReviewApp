using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Repository.Repos
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public Pokemon? GetPokemon(int pokemonId)
        {
            return _context.Pokemon.Where(p => p.Id == pokemonId).FirstOrDefault();
        }

        public Pokemon? GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name.Trim().ToLower() == name.Trim().ToLower())
                .FirstOrDefault();
        }

        public int GetPokemonRating(int pokemonId)
        {
            var review = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();

            if(review.Count < 1)
            {
                return 0;
            }
            else
            {
                return (int)review.Average(r => r.Rating);
            }
        }

        public bool PokemonExists(int pokemonId)
        {
            return _context.Pokemon.Any(p => p.Id == pokemonId);
        }

        public bool PokemonExists(string pokemonName)
        {
            return _context.Pokemon.Any(p => p.Name.Trim().ToLower() == pokemonName.Trim().ToLower());
        }

        public bool CreatePokemon(int owernId, int categoryId, Pokemon pokemon)
        {

            var owner = _context.Owners.Where(o => o.Id == owernId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (owner == null || category == null)
            {
                return false;
            }
            else
            {
                var pokemonOwner = new PokemonOwner()
                {
                    Owner = owner,
                    Pokemon = pokemon
                };

                _context.Add(pokemonOwner);

                var pokemonCategory = new PokemonCategory()
                {
                    Category = category,
                    Pokemon = pokemon
                };

                _context.Add(pokemonCategory);

                _context.Add(pokemon);

                return Save();
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
