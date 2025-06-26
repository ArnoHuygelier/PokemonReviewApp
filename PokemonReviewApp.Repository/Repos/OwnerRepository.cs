using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Repository.Repos
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PokemonDbContext _context;

        public OwnerRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners
                .OrderBy(o => o.LastName)
                .ToList();
        }

        public Owner? GetOwner(int ownerId)
        {
            return _context.Owners
                .Where(o => o.Id == ownerId)
                .FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
        {
            return _context.PokemonOwners
                .Where(po => po.PokemonId == pokemonId)
                .Select(po => po.Owner)
                .ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners
                .Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon)
                .ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool OwnerExists(string firstName, string lastName)
        {
            return _context.Owners
                .Where(o => o.FirstName.ToLower().Trim() == firstName.ToLower().Trim() && o.LastName.ToLower().Trim() == lastName.ToLower().Trim()).Any();
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
