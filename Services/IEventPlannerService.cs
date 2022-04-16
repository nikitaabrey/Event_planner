using EventPlanner.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        Event FindEventById(int id);

    }

}