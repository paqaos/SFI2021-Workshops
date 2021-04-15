using System;
using AutoMapper;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Events.Dto;

namespace SFI.Microservice.Events.BusinessLayer
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventItem, EventDto>();
        }
    }
}
