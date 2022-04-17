using Event_planner.Repositories;
using EventPlanner.Models;

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

          public void UpdateEvent(int UserId, int EventId, string EventName, string EventDesc, string RecurringId, DateTime StartDate, DateTime EndDate, TimeSpan StartTime, TimeSpan EndTime, bool IsFullDay)       
          {
            
            var Event = new Event {
                UserId = UserId,
                EventId = EventId,
                EventName = EventName,
                EventDesc = EventDesc,
                RecurringId = RecurringId,
                StartDate = StartDate,
                EndDate = EndDate,
                StartTime = StartTime,
                EndTime = EndTime,
                IsFullDay = IsFullDay,
            };

            this.EventPlannerRepo.UpdateEvent(Event);
            
            //var EventToUpdate = this.EventPlanRepository.findEvent(id);
            

            //var Event1 = this.EventPlanRepository.findEvent(_event);
            //this.EventPlanRepository.UpdateEvent(_event);
           
          }
    }
}