using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public partial class Event
    {
        public Event()
        {
           
        }

        public int EventId { get; set; }
        public int UserId { get; set; }
        public string RecurringId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string EventDesc { get; set; }
        public bool IsFullDay { get; set; }

        public virtual Calendar EndDateCalendar { get; set; }
        public virtual RecurringEvents RecurringEvents { get; set; }
        public virtual Calendar StartDateCalendar { get; set; }
        public virtual Users Users { get; set; }

    }
}
