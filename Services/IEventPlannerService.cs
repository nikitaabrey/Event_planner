using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        EventDTO FindEventById(int id);

        void UpdateEvent(EventDTO eventDTO);
        
        void CreateEvent(EventDTO Event);

        IEnumerable<EventDTO> FindEventsByUserId(int id, string date);
    }

}