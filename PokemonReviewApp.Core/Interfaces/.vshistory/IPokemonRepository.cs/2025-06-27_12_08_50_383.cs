using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon? GetPokemon(int pokemonId);
        Pokemon? GetPokemon(string name);
        int GetPokemonRating(int pokemonId);
        bool PokemonExists(int pokemonId);
        bool CreatePokemon(int owernId, int categoryId, Pokemon pokemon);
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();
    }
}
