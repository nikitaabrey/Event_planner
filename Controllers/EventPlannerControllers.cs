using Microsoft.AspNetCore.Mvc;
using Event_planner.Services;
using EventPlanner.Models;
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

        //PUT: api/EventPlanner
         [HttpPut("UpdateEvent")]
         public  async Task<ActionResult> UpdateEvent(int UserId, int EventId, string EventName, string EventDesc, string RecurringId, DateTime StartDate, DateTime EndDate, TimeSpan StartTime, TimeSpan EndTime, bool IsFullDay)
         {
             if (EventId == null)
             {
              return BadRequest();
             }
            else 
            { 
                  //StartTime =  DateTime.Now.TimeOfDay;
                  //EndTime = DateTime.Now.TimeOfDay;

                 this.EventPlannerService.UpdateEvent(UserId, EventId, EventName, EventDesc, RecurringId, StartDate, EndDate, StartTime, EndTime, IsFullDay);
                 return NoContent();
            }
         }     

    }
}