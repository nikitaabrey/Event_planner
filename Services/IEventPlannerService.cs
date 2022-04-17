using EventPlanner.Models;

namespace Event_planner.Services
{
    public interface IEventPlannerService
    {
        void DeleteEvent(int id);

        void UpdateEvent(int UserId, int EventId, string EventName, string EventDesc, string RecurringId, DateTime StartDate, DateTime EndDate, TimeSpan StartTime, TimeSpan EndTime, bool IsFullDay);

    }
}