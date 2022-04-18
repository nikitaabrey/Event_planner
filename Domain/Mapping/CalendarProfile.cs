using Event_planner.Domain.Models;
using EventPlanner.Models;

namespace Event_planner.Domain.Mapping
{
    public partial class CalendarProfile : AutoMapper.Profile
    {
        public CalendarProfile()
        {
            CreateMap<Calendar, CalendarDTO>()
                .ForMember(mem => mem.FullDate, opt => opt.MapFrom(s => s.FullDate.ToShortDateString()))
                .ForMember(mem => mem.FirstOfWeek, opt => opt.MapFrom(s => s != null ? s.FirstOfWeek.Value.ToShortDateString() : ""))
                .ForMember(mem => mem.LastOfWeek, opt => opt.MapFrom(s => s != null ? s.LastOfWeek.Value.ToShortDateString() : ""));


        }
        
    }
}
