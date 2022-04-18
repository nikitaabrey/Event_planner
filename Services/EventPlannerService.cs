using Event_planner.Repositories;
using EventPlanner.Models;
using EventPlanner.Domain.Models;
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

        public DateTime getDate(string Date) {
            return DateTime.ParseExact(Date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
        }
        
        public TimeSpan getTime(string Time) {
            return TimeSpan.ParseExact(Time, "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture);
        }

        public void CreateEvent(EventDTO EventDTO)
        {
            var Event = new Event {
                UserId = EventDTO.UserId,
                EventName = EventDTO.EventName,
                EventDesc = EventDTO.EventDesc,
                RecurringId = EventDTO.RecurringId,
                StartDate = getDate(EventDTO.StartDate),
                EndDate = getDate(EventDTO.EndDate),
                StartTime = getTime(EventDTO.StartTime),
                EndTime = getTime(EventDTO.EndTime),
                IsFullDay = EventDTO.IsFullDay,
            };
            
            this.EventPlannerRepo.CreateEvent(Event);

        }
    }
}