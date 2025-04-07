namespace StoreNet.Application.Dtos.Carts.CartItems;

public record UpdateCartItemDto(
    Guid ProductId,
    int NewQuantity
);
