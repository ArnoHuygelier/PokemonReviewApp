using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner? GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int pokemonId);
        ICollection<Pokemon> GetPokemonByOwner (int ownerId);
        bool OwnerExists(int ownerId);
        bool OwnerExists(string firstName, string lastName);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool Save();    
    }
}
