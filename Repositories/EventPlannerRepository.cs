using EventPlanner.Models;
using Event_planner.Data;

namespace Event_planner.Repositories
{
    public class EventPlannerRepository : IEventPlannerRepository
    {
        private EventPlannerContext context;

        public EventPlannerRepository(EventPlannerContext context)
        {
            this.context = context;
        }

        public void DeleteEvent(Event _event)
        {
            this.context.Remove(_event);
            this.context.SaveChanges();
        }

        public Event findEvent(int id)
        {
            return this.context.Find<Event>(id);
        }
    }
}