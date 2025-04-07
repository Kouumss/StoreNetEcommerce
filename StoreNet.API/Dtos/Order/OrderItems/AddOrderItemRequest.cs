using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Order.OrderItems;

public class AddOrderItemRequest
{
    [Required] public Guid ProductId { get; set; }
    [Required] public int Quantity { get; set; }
}

