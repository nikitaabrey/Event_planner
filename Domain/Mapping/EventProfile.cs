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
                .ForSourceMember(source => source.UserId, opt => opt.DoNotValidate());

            CreateMap<EventDTO, Event>();

        }

    }
}
