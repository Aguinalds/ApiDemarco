using ApiDemarco.Application.DTOs;

namespace ApiDemarco.Application.Helpers;

public class PaginationHelper
{
    public static async Task<PagedResult<T>> GetPagedResult<T>(IEnumerable<T> source, PaginationParams paginationParams)
    {
        var queryable = source.AsQueryable();

        var totalItems = queryable.Count();
        var items = await Task.Run(() =>
            queryable.Skip((paginationParams.pageNumber - 1) * paginationParams.pageSize)
                .Take(paginationParams.pageSize)
                .ToList());

        return new PagedResult<T>
        {
            items = items,
            totalItems = totalItems,
            pageNumber = paginationParams.pageNumber,
            pageSize = paginationParams.pageSize
        };
    }
}