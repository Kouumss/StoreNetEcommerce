using StoreNet.API.Dtos.Cart.CartItems;

namespace StoreNet.API.Dtos.Cart;

public record CartResponse(
    Guid Id,
    Guid UserId,
    decimal TotalPrice,
    List<CartItemResponse> Items
);
