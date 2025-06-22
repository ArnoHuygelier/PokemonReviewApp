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
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
