using StoreNet.API.Dtos.Product;

namespace StoreNet.API.Dtos.Cart.CartItems;

public class CartItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

