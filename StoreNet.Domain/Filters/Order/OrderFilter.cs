using StoreNet.Domain.Entities;

namespace StoreNet.Domain.Filters.Order;

public record OrderFilter(
    Guid? UserId = null,
    OrderStatus? Status = null,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    string SortBy = "CreatedAtDesc",
    int PageIndex = 1,
    int PageSize = 10);


