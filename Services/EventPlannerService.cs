using Event_planner.Repositories;

namespace Event_planner.Services
{
    public class EventPlannerService : IEventPlannerService
    {
        private IEventPlannerRepository EventPlannerRepo;
        public EventPlannerService(IEventPlannerRepository EventPlannerRepo){
            this.EventPlannerRepo = EventPlannerRepo;
        }
        public void DeleteEvent(int id)
        {
            var Event = this.EventPlannerRepo
           .findEvent(id);
            if (Event == null)
            {
                throw new Exception($"Could not find Event {id}");
            }
            this.EventPlannerRepo.DeleteEvent(Event);

        }
    }
}