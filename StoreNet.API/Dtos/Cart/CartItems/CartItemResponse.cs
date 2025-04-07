namespace StoreNet.API.Dtos.Cart.CartItems;

public record CartItemResponse(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);
