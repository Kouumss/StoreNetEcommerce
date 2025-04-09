using StoreNet.Application.Dtos.Products;

namespace StoreNet.Application.Dtos.Carts.CartItems;
public class CartItemDto
{
    public Guid Id { get; set; } 
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
