using System;
using AutoMapper;
using EventPlanner.Domain.Models;
using EventPlanner.Models;

namespace EventPlanner.Domain.Mapping
{
    public partial class EventProfile
        : AutoMapper.Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>()
                .ForSourceMember(source => source.UserId, opt => opt.DoNotValidate())
                .ForMember(source => source.StartDate, opt => opt.MapFrom(s => s != null ? s.StartDate.Date.ToShortDateString() : ""))
                .ForMember(source => source.EndDate, opt => opt.MapFrom(s => s != null ? s.EndDate.Date.ToShortDateString() : ""));

            CreateMap<EventDTO, Event>();

        }

    }
}
