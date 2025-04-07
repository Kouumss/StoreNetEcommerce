using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Order.OrderItems;

public record UpdateOrderItemRequest(
    [Required] Guid ProductId ,
    [Required]  int NewQuantity );

