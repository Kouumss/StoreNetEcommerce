using StoreNet.Application.Dtos.Orders.OrderItems;

namespace StoreNet.Application.Dtos.Orders;

public record OrderDto(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    string Status,
    decimal TotalAmount,
    string ShippingAddress,
    IReadOnlyList<OrderItemDto> OrderItems
);
