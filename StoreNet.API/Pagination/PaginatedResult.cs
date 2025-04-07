namespace StoreNet.API.Pagination;

public record PaginatedResult<T>(
     int PageIndex,
     int PageSize ,
     int TotalCount ,
     IReadOnlyList<T> Data);

