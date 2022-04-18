using Microsoft.AspNetCore.Mvc;
using Event_planner.Services;
using EventPlanner.Domain.Models;

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

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO EventDTO)
        {
            this.EventPlannerService.CreateEvent(EventDTO);
            string removalConfirmation = "Created Event";
            return new ObjectResult(removalConfirmation);

        }

    }
}