using System.ComponentModel.DataAnnotations;
using StoreNet.API.Dtos.Order.OrderItems;

namespace StoreNet.API.Dtos.Order;

public record CreateOrderRequest
{
    [Required]
    public Guid UserId { get; init; }

    [Required]
    [MaxLength(200)]
    public string ShippingAddress { get; init; }

    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; init; }

    public string? Notes { get; init; }

    [Required]
    public IReadOnlyList<AddOrderItemRequest> OrderItems { get; init; }

    public CreateOrderRequest(Guid userId, string shippingAddress, string paymentMethod, string? notes, IReadOnlyList<AddOrderItemRequest> orderItems)
    {
        UserId = userId;
        ShippingAddress = shippingAddress;
        PaymentMethod = paymentMethod;
        Notes = notes;
        OrderItems = orderItems;
    }
}
