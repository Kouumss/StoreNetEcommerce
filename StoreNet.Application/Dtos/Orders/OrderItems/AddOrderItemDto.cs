namespace StoreNet.Application.Dtos.Orders.OrderItems;

public record AddOrderItemDto(
    Guid ProductId,
    int Quantity
);
