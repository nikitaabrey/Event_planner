using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Domain.Models
{
    public partial class EventDTO
    {
        public int EventId { get; set; }

        public string RecurringId { get; set; }

        public int UserId { get; set; }
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string EventName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string EventDesc { get; set; }

        public bool IsFullDay { get; set; }


    }
}
