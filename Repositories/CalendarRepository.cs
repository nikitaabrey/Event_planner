using Event_planner.Data;
using EventPlanner.Data.Queries;
using EventPlanner.Models;
using System.Linq.Expressions;

namespace Event_planner.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly EventPlannerContext context;

        public CalendarRepository(EventPlannerContext context) {
        
            this.context = context;
        }
        public Calendar get(DateTime fullDate)
        {
           return context.Calendars.Find(fullDate);
        }

        public IEnumerable<Calendar> get(Expression<Func<Calendar, bool>> filter)
        {
            return context.Calendars.Where(filter).ToList();
            
        }
    }
}
