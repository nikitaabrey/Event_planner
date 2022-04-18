
using EventPlanner.Models;

namespace Event_planner.Repositories
{
    public interface IEventPlannerRepository
    {
        void DeleteEvent(Event _event);
        
        void CreateEvent(Event _event);

        Event findEvent(int id);
    }
}