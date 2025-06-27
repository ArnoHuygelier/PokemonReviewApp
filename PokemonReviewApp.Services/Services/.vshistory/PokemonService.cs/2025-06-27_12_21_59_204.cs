using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class PokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public ICollection<Pokemon> GetAllPokemons()
        {
            return _pokemonRepository.GetPokemons();
        }

        public Pokemon? GetPokemonById(int id)
        {
            return _pokemonRepository.GetPokemon(id);
        }

        public Pokemon? GetPokemonByName(string name)
        {
            return _pokemonRepository.GetPokemon(name);
        }

        public int GetPokemonRating(int id)
        {
            return _pokemonRepository.GetPokemonRating(id);
        }

        public bool DoesPokemonExist(int id)
        {
            return _pokemonRepository.PokemonExists(id);
        }

        public bool DoesPokemonExist(string pokemokName)
        {
            return _pokemonRepository.PokemonExists(pokemokName);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            if (pokemon == null)
            {
                return false;
            }
            else
            {
                return _pokemonRepository.CreatePokemon(ownerId, categoryId, pokemon);
            }
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            if (pokemon == null)
            {
                return false;
            }
            else
            {
                return _pokemonRepository.UpdatePokemon(pokemon);
            }
        }
    }
}
