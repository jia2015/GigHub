using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

        }

        protected override void Configure()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Gig, GigDto>();
        }
        
    }
}