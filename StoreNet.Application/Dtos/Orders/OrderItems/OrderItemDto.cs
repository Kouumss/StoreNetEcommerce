namespace StoreNet.Application.Dtos.Orders.OrderItems;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal Price,
    decimal Discount
);