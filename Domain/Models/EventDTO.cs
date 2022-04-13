using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Domain.Models
{
    public partial class EventDTO
    {
        public int EventId { get; set; }

        public string RecurringId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string EventName { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string EventDesc { get; set; }

        public bool IsFullDay { get; set; }


    }
}
