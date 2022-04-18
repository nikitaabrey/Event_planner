using EventPlanner.Models;
using System.Linq.Expressions;

namespace Event_planner.Repositories
{
    public interface ICalendarRepository
    {
        Calendar get(DateTime fullDate);
        IEnumerable<Calendar> get(Expression<Func<Calendar, bool>> filter);
    }
}
