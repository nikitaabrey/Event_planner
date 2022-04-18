namespace EventPlanner.Models
{
    public partial class Calendar
    {
        public Calendar()
        {
            EndDateEvents = new HashSet<Event>();
            StartDateEvents = new HashSet<Event>();
        }

        public DateTime FullDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int? DayOfWeek { get; set; }
        public int? WeekOfMonth { get; set; }
        public DateTime? FirstOfWeek { get; set; }
        public DateTime? LastOfWeek { get; set; }
        public int? DayOfYear { get; set; }

        public virtual ICollection<Event> EndDateEvents { get; set; }
        public virtual ICollection<Event> StartDateEvents { get; set; }


    }
}
