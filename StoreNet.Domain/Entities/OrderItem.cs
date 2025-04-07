using StoreNet.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; } 
}
