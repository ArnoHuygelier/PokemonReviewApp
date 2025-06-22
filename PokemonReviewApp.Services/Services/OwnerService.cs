using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class OwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public ICollection<Owner> GetOwners()
        {
            return _ownerRepository.GetOwners();
        }

        public Owner? GetOwner(int ownerId)
        {
            return _ownerRepository.GetOwner(ownerId);
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
        {
            return _ownerRepository.GetOwnerOfAPokemon(pokemonId);
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _ownerRepository.GetPokemonByOwner(ownerId);
        }

        public bool OwnerExists(int ownerId)
        {
            return _ownerRepository.OwnerExists(ownerId);
        }
    }
}
