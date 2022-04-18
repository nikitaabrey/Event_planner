using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Queries
{
    public static partial class RecurringEventsExtensions
    {
        #region Generated Extensions
        public static EventPlanner.Models.RecurringEvents GetByKey(this IQueryable<EventPlanner.Models.RecurringEvents> queryable, string recurringId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.RecurringEvents> dbSet)
                return dbSet.Find(recurringId);

            return queryable.FirstOrDefault(q => q.RecurringId == recurringId);
        }

        public static ValueTask<EventPlanner.Models.RecurringEvents> GetByKeyAsync(this IQueryable<EventPlanner.Models.RecurringEvents> queryable, string recurringId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.RecurringEvents> dbSet)
                return dbSet.FindAsync(recurringId);

            var task = queryable.FirstOrDefaultAsync(q => q.RecurringId == recurringId);
            return new ValueTask<EventPlanner.Models.RecurringEvents>(task);
        }

        #endregion

    }
}
