using System;
using AutoMapper;
using EventPlanner.Domain.Models;
using EventPlanner.Models;

namespace EventPlanner.Domain.Mapping
{
    public partial class EventProfile
        : AutoMapper.Profile
    {

        public class DateFormatter : AutoMapper.IValueConverter<DateTime, DateOnly>
        {
            

            public DateOnly Convert(DateTime sourceMember, ResolutionContext context)
            {
                return DateOnly.FromDateTime(sourceMember);
            }
        }



        public EventProfile()
        {
            CreateMap<Event, EventDTO>()
                .ForSourceMember(source => source.UserId, opt => opt.DoNotValidate());

            CreateMap<EventDTO, Event>();

        }

    }
}
