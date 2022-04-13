using Microsoft.AspNetCore.Mvc;
using EventPlanner.Models;
using Event_planner.Data;
using AutoMapper;

namespace Event_planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPlannerController : ControllerBase
    {
        private readonly EventPlannerContext EventPlannerContext;
        private readonly IMapper mapper;
        public EventPlannerController(EventPlannerContext context, IMapper mapper)
        {
            this.EventPlannerContext = context;
            this.mapper = mapper;
        }

        // DELETE: api/EventPlanner/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            var Event = this.EventPlannerContext
           .Set<Event>()
           .FirstOrDefault(p => p.EventId == id);
            if (Event == null)
            {
                return NotFound();
            }
            this.EventPlannerContext.Remove(Event);

            this.EventPlannerContext.SaveChanges();

            string removalConfirmation = $"Removed Event {id}";
            return new ObjectResult(removalConfirmation);

        }

    }
}