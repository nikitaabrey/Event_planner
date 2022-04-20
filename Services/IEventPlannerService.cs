using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        Event FindEventById(int id);

        void UpdateEvent(EventDTO eventDTO);

        void CreateEvent(EventDTO Event);

        IEnumerable<EventDTO> GetWeekEvents(int userId, String date);
    }

}