using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);
        
        void CreateEvent(EventDTO Event);
    }
}