using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Cart.CartItems;

public record UpdateCartItemQuantityRequest(
    [Range(1, 100)] int NewQuantity);
