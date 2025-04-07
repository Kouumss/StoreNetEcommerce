namespace StoreNet.Application.Dtos.Carts.CartItems;

// Commandes
public record AddCartItemDto(
    Guid ProductId,
    int Quantity
);

