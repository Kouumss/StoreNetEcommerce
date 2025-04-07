using StoreNet.Application.Dtos.Carts.CartItems;

namespace StoreNet.Application.Dtos.Carts;
public record CartDto(
    Guid Id,
    Guid UserId,
    decimal TotalPrice,
    List<CartItemDto> Items
);
