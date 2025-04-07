namespace StoreNet.Application.Dtos.Orders.OrderItems;


public record UpdateOrderItemDto(
Guid ProductId,
int NewQuantity
);

