using Event_planner.Models;
using Event_planner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Event_planner.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;

        private readonly List<Member> lstMember = new List<Member>()
        {
            new Member{Id=1, Name="John" },
            new Member {Id=2, Name="Jack" },
            new Member{Id=3, Name="Sally"}
        };
        public MembersController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }
        // GET: api/<MembersController>
        [HttpGet]
        public IEnumerable<Member> AllMembers()
        {
            return lstMember;
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public Member MemberByid(int id)
        {
            return lstMember.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
