using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        void UpdateEvent(UpdateDTO updateDTO);
        
        void CreateEvent(EventDTO Event);
    }
}