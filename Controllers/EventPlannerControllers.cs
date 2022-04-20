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
        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            this.EventPlannerService.DeleteEvent(id);

            string removalConfirmation = $"Removed Event {id}";
            return new ObjectResult(removalConfirmation);

        }

        [HttpGet("GetEvent/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            EventDTO EventObject = this.EventPlannerService.FindEventById(id);
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
        
        [HttpGet("GetEventsByDay/{id}/{date}")]
        public async Task<IActionResult> GetEventsByDay(int id, string date)
        {
            IEnumerable<EventDTO> EventsObject = this.EventPlannerService.FindEventsByUserId(id, date);
            if (EventsObject == null)
            {
                return NotFound();
            }
            return new ObjectResult(EventsObject);

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
            try 
            { 
                this.EventPlannerService.CreateEvent(EventDTO);
                return new ObjectResult("Created Event");
            }
            catch(ArgumentException ex)
            {
               
                return BadRequest();
            }

        }


    }
}