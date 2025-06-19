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
        private readonly IPokemonRepository _repository;

        public PokemonService(IPokemonRepository repository)
        {
            _repository = repository;
        }
    }
}
