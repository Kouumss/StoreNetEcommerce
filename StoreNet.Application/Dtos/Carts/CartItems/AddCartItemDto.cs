namespace StoreNet.Application.Dtos.Carts.CartItems;

public record AddCartItemDto(
    Guid UserId,
    Guid ProductId,
    int Quantity
);
