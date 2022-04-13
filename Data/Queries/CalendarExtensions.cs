using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Queries
{
    public static partial class CalendarExtensions
    {
        #region Generated Extensions
        public static EventPlanner.Models.Calendar GetByKey(this IQueryable<EventPlanner.Models.Calendar> queryable, DateTime fullDate)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Calendar> dbSet)
                return dbSet.Find(fullDate);

            return queryable.FirstOrDefault(q => q.FullDate == fullDate);
        }

        public static ValueTask<EventPlanner.Models.Calendar> GetByKeyAsync(this IQueryable<EventPlanner.Models.Calendar> queryable, DateTime fullDate)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Calendar> dbSet)
                return dbSet.FindAsync(fullDate);

            var task = queryable.FirstOrDefaultAsync(q => q.FullDate == fullDate);
            return new ValueTask<EventPlanner.Models.Calendar>(task);
        }

        #endregion

    }
}
