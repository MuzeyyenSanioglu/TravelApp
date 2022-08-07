using AutoMapper;
using TravelApp.API.Model;
using TravelApp.Domain.Entities;

namespace TravelApp.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Travel, CreateTravelDto>().ReverseMap();
            CreateMap<Travel, TravelsDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
