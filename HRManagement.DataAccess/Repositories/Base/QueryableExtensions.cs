using Microsoft.EntityFrameworkCore;

namespace HRManagement.DataAccess.Repositories.Base
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByWithColumnName<T>(this IQueryable<T> source, string sortColumn)
            => source.OrderBy(x => EF.Property<T>(x, sortColumn));

        public static IOrderedQueryable<T> OrderByWithColumnNameDescending<T>(this IQueryable<T> source, string sortColumn)
            => source.OrderByDescending(x => EF.Property<T>(x, sortColumn));
    }
}
