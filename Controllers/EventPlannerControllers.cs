using Microsoft.AspNetCore.Mvc;
using Event_planner.Services;
using EventPlanner.Models;
using EventPlanner.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

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

        [HttpPut("UpdateEvent")]
        public  async Task<ActionResult> UpdateEvent([FromBody] UpdateDTO updateDTO)
        {
            try 
            { 
                this.EventPlannerService.UpdateEvent(updateDTO);
                string UpdateStatus = $"Updated Event";
                return new ObjectResult(UpdateStatus);
            }
            catch(ArgumentException ex)
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