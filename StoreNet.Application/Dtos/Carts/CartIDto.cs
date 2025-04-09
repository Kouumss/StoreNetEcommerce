
using StoreNet.Application.Dtos.Carts.CartItems;

namespace StoreNet.Application.Dtos.Carts;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
}





