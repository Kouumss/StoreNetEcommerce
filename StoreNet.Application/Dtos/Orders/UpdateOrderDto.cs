namespace StoreNet.Application.Dtos.Orders;

public record UpdateOrderDto(
    string Status,
    string? TrackingNumber,
    string? Notes
);
