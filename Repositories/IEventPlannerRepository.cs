
using EventPlanner.Models;
using System.Linq.Expressions;

namespace Event_planner.Repositories
{
    public interface IEventPlannerRepository
    {
        void DeleteEvent(Event _event);

        void CreateEvent(Event _event);

        Event findEvent(int id);

        Task UpdateEvent(Event _event);

        IEnumerable<Event> findUserEvents(int UserId);
        IEnumerable<Event> get(Expression<Func<Event, bool>> filter);
    }
}