using AutoMapper;
using travelapi.Domain.Models;
using travelapi.Domain.Dto;
using travelapi.Controllers;

namespace travelapi.Domain.AutoMappers
{
    public class AutoMappersProfile : Profile
    {
        public AutoMappersProfile() 
        {
            CreateMap<Activity, ActivityDto>()
                .ReverseMap();
            CreateMap<Cost, CostDto>()
                .ReverseMap();
            CreateMap<Destination, DestinationDto>()
                .ReverseMap();
            CreateMap<HotelDto, Hotel>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<Local, LocalDto>();
            CreateMap<LocalDto, Local>();
            CreateMap<Reservation, ReservationDto>()
                .ReverseMap();
            CreateMap<Restaurant, RestaurantDto>()
                .ReverseMap();
            CreateMap<TouristAttraction, TouristAttractionDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ReverseMap();

        }
    }
}
