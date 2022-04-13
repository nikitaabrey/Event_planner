#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;
using Event_planner.Data;
using AutoMapper;
using EventPlanner.Domain.Models;
using AutoMapper.QueryableExtensions;

namespace Event_planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly EventPlannerContext EventPlannerContext;
        private readonly IMapper mapper;
        public TestController(EventPlannerContext context, IMapper mapper)
        {
            this.EventPlannerContext = context;
            this.mapper=mapper;
        }



        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEvent(int id)
        {
            var model =  this.EventPlannerContext
           .Set<Event>()
           .AsNoTracking()
           .Where(p => p.EventId == id)
           .ProjectTo<EventDTO>(mapper.ConfigurationProvider);
            return new ObjectResult(model);

        }


    
    }
}
