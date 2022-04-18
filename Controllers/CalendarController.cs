using Event_planner.Domain.UserExceptions;
using Event_planner.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Event_planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService service;
        public CalendarController(ICalendarService service) { 
            this.service = service;
        }



        [HttpGet]
        [Route("day/")]
        public IActionResult getDay([FromQuery] string date)
        {
            try
            {
                return new ObjectResult(service.getDay(date));
            }
            catch (ArgumentException ex) {
                return BadRequest();

            } catch (ResourceNotFoundException ex) { 
                    return NotFound();
            }
        }


        
        [HttpGet]
        [Route("month/")]
        public IActionResult getMonth([FromQuery] int month,[FromQuery] int year)
        {
            try
            {
                return new ObjectResult(service.getMonth(month, year));
            } catch (ArgumentException ex)
            {
                return BadRequest();

            }
        }
        

        [HttpGet, ActionName("getWeek")]
        [Route("week/")]

        public IActionResult getWeek([FromQuery] string date)
        {
            try { 
                return new ObjectResult(service.getWeek(date));

            }  catch (ArgumentException ex) {
                return BadRequest();

            } catch (ResourceNotFoundException ex) { 
                            return NotFound();
            }
        }

    }
}
