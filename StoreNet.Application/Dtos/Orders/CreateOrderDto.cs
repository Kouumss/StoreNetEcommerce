using StoreNet.Application.Dtos.Orders.OrderItems;

namespace StoreNet.Application.Dtos.Orders;

public record CreateOrderDto(
    Guid UserId,
    string ShippingAddress,
    string PaymentMethod,
    string? Notes,
    IReadOnlyList<AddOrderItemDto> OrderItems
);
