using Microsoft.AspNetCore.Mvc;
using Event_planner.Services;
using EventPlanner.Models;
using Newtonsoft.Json;

namespace Event_planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPlannerController : ControllerBase
    {
        private readonly IEventPlannerService EventPlannerService;
        public EventPlannerController(IEventPlannerService EventPlannerService)
        {
            this.EventPlannerService = EventPlannerService;
        }

        // DELETE: api/EventPlanner/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            this.EventPlannerService.DeleteEvent(id);

            string removalConfirmation = $"Removed Event {id}";
            return new ObjectResult(removalConfirmation);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            Event EventObject = this.EventPlannerService.FindEventById(id);
            if (EventObject == null)
            {
                return NotFound();
            }
            return new ObjectResult(EventObject);

        }

    }
}