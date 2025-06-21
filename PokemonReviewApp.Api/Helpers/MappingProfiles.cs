using AutoMapper;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;

namespace PokemonReviewApp.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
