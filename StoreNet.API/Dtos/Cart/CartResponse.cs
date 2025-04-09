using StoreNet.API.Dtos.Cart.CartItems;
using StoreNet.Application.Dtos.Carts.CartItems;

namespace StoreNet.API.Dtos.Cart;


public class CartResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
}