using System;
using System.Collections.Generic;

namespace EventPlanner.Models
{
    public partial class RecurringEvents
    {
        public RecurringEvents()
        {
            Events = new HashSet<Event>();
        }

        public string RecurringId { get; set; }
        public string RecurringDesc { get; set; }
        public virtual ICollection<Event> Events { get; set; }


    }
}
