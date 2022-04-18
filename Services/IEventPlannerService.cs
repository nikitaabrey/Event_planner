using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        void UpdateEvent(EventDTO eventDTO);
    }
}