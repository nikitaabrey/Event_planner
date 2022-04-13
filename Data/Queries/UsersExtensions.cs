using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Data.Queries
{
    public static partial class UsersExtensions
    {
        #region Generated Extensions
        public static EventPlanner.Models.Users GetByKey(this IQueryable<EventPlanner.Models.Users> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Users> dbSet)
                return dbSet.Find(userId);

            return queryable.FirstOrDefault(q => q.UserId == userId);
        }

        public static ValueTask<EventPlanner.Models.Users> GetByKeyAsync(this IQueryable<EventPlanner.Models.Users> queryable, int userId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<EventPlanner.Models.Users> dbSet)
                return dbSet.FindAsync(userId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId);
            return new ValueTask<EventPlanner.Models.Users>(task);
        }

        #endregion

    }
}
