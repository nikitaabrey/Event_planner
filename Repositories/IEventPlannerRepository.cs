
using EventPlanner.Models;

namespace Event_planner.Repositories
{
    public interface IEventPlannerRepository
    {
        void DeleteEvent(Event _event);
        Event findEvent(int id);

        void UpdateEvent(Event _event);
    }
}