using System;
using System.Collections.Generic;

namespace EventPlanner.Models
{
    public partial class Users
    {
        public Users()
        {
            Events = new HashSet<Event>();
        }

        public string UserName { get; set; }
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Event> Events { get; set; }


    }
}
