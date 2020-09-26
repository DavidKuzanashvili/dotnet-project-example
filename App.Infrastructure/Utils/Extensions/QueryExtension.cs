using App.Domain.Models.Shared.Pagination;
using System.Linq;

namespace App.Infrastructure.Utils.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagingParameters paging)
            where T : class, new()
        {
            query = query
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            return query;
        }
    }
}
