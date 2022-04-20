namespace Event_planner.Domain.Models
{
    public class CalendarDTO
    {

        public string? FullDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int? DayOfWeek { get; set; }
        public int? WeekOfMonth { get; set; }
        public string? FirstOfWeek { get; set; }
        public string? LastOfWeek { get; set; }
        public int? DayOfYear { get; set; }
    }
}
