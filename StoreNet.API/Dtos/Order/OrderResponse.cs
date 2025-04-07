using StoreNet.API.Dtos.Order.OrderItems;

namespace StoreNet.API.Dtos.Order;

public record OrderResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string Status { get; init; } = string.Empty;
    public string ShippingAddress { get; init; } = string.Empty;
    public IReadOnlyList<OrderItemResponse> OrderItems { get; init; }

    public OrderResponse(Guid id, Guid userId, DateTime orderDate, string status, decimal totalAmount, string shippingAddress, IReadOnlyList<OrderItemResponse> orderItems)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = totalAmount;
        ShippingAddress = shippingAddress;
        OrderItems = orderItems;
    }
}
