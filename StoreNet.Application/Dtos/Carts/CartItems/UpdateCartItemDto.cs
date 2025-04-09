namespace StoreNet.Application.Dtos.Carts.CartItems;

public record UpdateCartItemDto(
    Guid UserId,
    Guid ProductId,
    int NewQuantity);