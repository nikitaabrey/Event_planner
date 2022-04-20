using Microsoft.AspNetCore.Mvc;
using Event_planner.Services;
using EventPlanner.Models;
using EventPlanner.Domain.Models;

namespace Event_planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPlannerController : ControllerBase
    {
        private Event _event;
        public Event Event => _event;

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

        [HttpGet("GetUserWeekEvents")]
        public IActionResult GetWeekEvents(int userId, String date)
        {
            IEnumerable<EventDTO> WeekEvents = this.EventPlannerService.GetWeekEvents(userId, date);

            return new ObjectResult(WeekEvents);

        }

        [HttpPut("UpdateEvent")]
        public async Task<ActionResult> UpdateEvent([FromBody] EventDTO eventDTO)
        {
            try
            {
                this.EventPlannerService.UpdateEvent(eventDTO);
                string UpdateStatus = $"Updated Event";
                return new ObjectResult(UpdateStatus);
            }
            catch (ArgumentException ex)
            {

                return BadRequest();
            }
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO EventDTO)
        {
            this.EventPlannerService.CreateEvent(EventDTO);
            return new ObjectResult("Created Event");

        }


    }
}