using AutoMapper;
using Event_planner.Repositories;
using EventPlanner.Models;
using EventPlanner.Domain.Models;
namespace Event_planner.Services
{
    public class EventPlannerService : IEventPlannerService
    {
        private IEventPlannerRepository EventPlannerRepo;

        private readonly IMapper mapper;
        public EventPlannerService(IEventPlannerRepository EventPlannerRepo, IMapper mapper){
            this.EventPlannerRepo = EventPlannerRepo;
            this.mapper = mapper;
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

        public void CreateEvent(EventDTO EventDTO)
        {
            var Event = mapper.Map<Event>(EventDTO);
            
            this.EventPlannerRepo.CreateEvent(Event);

        }
    }
}