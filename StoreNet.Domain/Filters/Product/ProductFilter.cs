namespace StoreNet.Domain.Filters.Product;

public record ProductFilter(
    List<string>? Brand,
    List<string>? Category,
    string? SearchTerm,
    string? SortBy,
    int PageIndex = 1,
    int PageSize = 12
    );