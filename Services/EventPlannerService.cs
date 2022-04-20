using AutoMapper;
using Event_planner.Repositories;
using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Services
{
    public class EventPlannerService : IEventPlannerService
    {
        private IEventPlannerRepository EventPlannerRepo;
        private readonly ICalendarService calenderService;
        private readonly IMapper mapper;

        public EventPlannerService(IEventPlannerRepository EventPlannerRepo, IMapper mapper, ICalendarService calenderService)
        {
            this.EventPlannerRepo = EventPlannerRepo;
            this.mapper = mapper;
            this.calenderService = calenderService;
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

        public EventDTO FindEventById(int id)
        {
            var EventInstance = this.EventPlannerRepo.findEvent(id);
            return mapper.Map<EventDTO>(EventInstance);
        }

        public IEnumerable<EventDTO> FindEventsByUserId(int id, string date)
        {
            IEnumerable<Event> Events = this.EventPlannerRepo.get(filter: s => s.UserId == id && s.StartDate == getDate(date));

            return  mapper.Map<IEnumerable<EventDTO>>(Events);
        }

        public DateTime getDate(string Date)
        {
            return DateTime.Parse(Date);
        }

        public TimeSpan getTime(string Time)
        {
            return TimeSpan.Parse(Time);
        }

        public void UpdateEvent(EventDTO eventDTO)
        {

            var ev = this.EventPlannerRepo.findEvent(eventDTO.EventId);


            if (ev == null)
            {
                throw new Exception("Event does not exist!");
            }
            else
            {
                ev.UserId = eventDTO.UserId;
                ev.EventId = eventDTO.EventId;
                ev.EventName = eventDTO.EventName;
                ev.EventDesc = eventDTO.EventDesc;
                ev.RecurringId = eventDTO.RecurringId;
                ev.StartDate = getDate(eventDTO.StartDate);
                ev.EndDate = getDate(eventDTO.EndDate);
                ev.StartTime = getTime(eventDTO.StartTime);
                ev.EndTime = getTime(eventDTO.EndTime);
                ev.IsFullDay = eventDTO.IsFullDay;

                this.EventPlannerRepo.UpdateEvent(ev);
            }

        }

        public void CreateEvent(EventDTO EventDTO)
        {
            var Event = mapper.Map<Event>(EventDTO);

            this.EventPlannerRepo.CreateEvent(Event);

        }

        public IEnumerable<EventDTO> GetWeekEvents(int userId, String date)
        {
            var Week = this.calenderService.getDay(date);
            var UserEvents = this.EventPlannerRepo.findUserEvents(userId);
            var WeekEvents = UserEvents.Where(e => e.StartDate >= getDate(Week.FirstOfWeek) && e.StartDate <= getDate(Week.LastOfWeek));

            return mapper.Map<IEnumerable<EventDTO>>(WeekEvents);
        }
    }
}