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
            CreateMap<Hotel, HotelDto>()
                .ReverseMap();
            CreateMap<Local, LocalDto>()
                .ReverseMap();
            CreateMap<Reservation, ReservationDto>()
                .ReverseMap();
            CreateMap<Restaurant, RestaurantDto>()
                .ReverseMap();
            CreateMap<TouristAttraction, TouristAttractionDto>()
                .ReverseMap();
            CreateMap<TypeRoom, TypeRoomDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ReverseMap();

        }
    }
}
