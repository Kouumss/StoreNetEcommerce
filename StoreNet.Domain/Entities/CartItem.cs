namespace StoreNet.Domain.Entities;

public class CartItem : BaseEntity
{
    public CartItem(Guid productId, int quantity, decimal price)
    {   
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Navigation properties
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}