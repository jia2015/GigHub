using AutoMapper;
using GigHub.Data.Dtos;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Gig, GigDto>();

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<ApplicationUser, UserDto>();
            //    cfg.CreateMap<Notification, NotificationDto>();
            //    cfg.CreateMap<Genre, GenreDto>();
            //    cfg.CreateMap<Gig, GigDto>();
            //});
        }
    }
}