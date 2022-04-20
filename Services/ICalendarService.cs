using Event_planner.Domain.Models;
using EventPlanner.Models;

namespace Event_planner.Services
{
    public interface ICalendarService
    {
        CalendarDTO getDay(string fullDate);
        IEnumerable<CalendarDTO> getMonth(int mont, int year);
        IEnumerable<CalendarDTO> getWeek(string fullDate);
    }
}
