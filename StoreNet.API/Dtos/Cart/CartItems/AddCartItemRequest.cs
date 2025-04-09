using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Cart.CartItems;


public record AddCartItemRequest(
    [Required] Guid ProductId,
    [Range(1, 100)] int Quantity);
