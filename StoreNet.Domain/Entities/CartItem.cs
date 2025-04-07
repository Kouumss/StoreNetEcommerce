using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreNet.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public string PictureUrl { get; private set; }
    public string BrandName { get; private set; }
    public decimal SubTotal => UnitPrice * Quantity;

    [Required]
    public Guid CartId { get; set; }

    [ForeignKey("CartId")]
    public Cart Cart { get; set; }


    public static CartItem Create(Guid productId, int quantity, decimal unitPrice, decimal discountPercent)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        return new CartItem
        {
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            DiscountPercent = discountPercent
        };
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public decimal CalculateLineTotal()
    {
        return UnitPrice * Quantity * (1 - DiscountPercent / 100);
    }
}