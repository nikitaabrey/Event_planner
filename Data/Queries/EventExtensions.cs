using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Queries
{
    public static partial class EventExtensions
    {
        #region Generated Extensions
        public static IQueryable<EventPlanner.Models.Event> ByEndDate(this IQueryable<EventPlanner.Models.Event> queryable, DateTime endDate)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.EndDate == endDate);
        }

        public static EventPlanner.Models.Event GetByKey(this IQueryable<EventPlanner.Models.Event> queryable, int eventId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Event> dbSet)
                return dbSet.Find(eventId);

            return queryable.FirstOrDefault(q => q.EventId == eventId);
        }

        public static ValueTask<EventPlanner.Models.Event> GetByKeyAsync(this IQueryable<EventPlanner.Models.Event> queryable, int eventId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Event> dbSet)
                return dbSet.FindAsync(eventId);

            var task = queryable.FirstOrDefaultAsync(q => q.EventId == eventId);
            return new ValueTask<EventPlanner.Models.Event>(task);
        }

        public static IQueryable<EventPlanner.Models.Event> ByRecurringId(this IQueryable<EventPlanner.Models.Event> queryable, string recurringId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.RecurringId == recurringId);
        }

        public static IQueryable<EventPlanner.Models.Event> ByStartDate(this IQueryable<EventPlanner.Models.Event> queryable, DateTime startDate)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StartDate == startDate);
        }

        public static IQueryable<EventPlanner.Models.Event> ByUserId(this IQueryable<EventPlanner.Models.Event> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.UserId == userId);
        }

        #endregion

    }
}
