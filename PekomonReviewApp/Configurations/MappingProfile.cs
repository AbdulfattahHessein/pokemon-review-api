using AutoMapper;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Reviewer, ReviewerDto>().ReverseMap();
        }

    }

}
