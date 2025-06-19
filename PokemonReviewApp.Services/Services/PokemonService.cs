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
        private readonly IRepository<Pokemon> _repository;

        public PokemonService(IRepository<Pokemon> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Pokemon>> GetPokemons()
        {
            return await _repository.GetAllAsync();
        }
    }
}
