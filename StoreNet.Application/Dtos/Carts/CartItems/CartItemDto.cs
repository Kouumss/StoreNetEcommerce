namespace StoreNet.Application.Dtos.Carts.CartItems;
public record CartItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);