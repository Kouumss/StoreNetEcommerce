namespace StoreNet.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; }
    public List<CartItem> Items { get; private set; } = new();

    public static Cart Create(Guid userId)
    {
        return new Cart
        {
            UserId = userId
        };
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice, decimal discountPercent)
    {
        var existingItem = Items.FirstOrDefault(i => i.Id == productId);

        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            Items.Add(CartItem.Create(productId, quantity, unitPrice, discountPercent));
        }

        UpdateTimestamps();
    }

    public void UpdateItemQuantity(Guid productId, int newQuantity)
    {
        var item = GetItem(productId);
        item.UpdateQuantity(newQuantity);
        UpdateTimestamps();
    }

    public void RemoveItem(Guid productId)
    {
        var item = GetItem(productId);
        Items.Remove(item);
        UpdateTimestamps();
    }

    public void Clear()
    {
        Items.Clear();
        UpdateTimestamps();
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(item => item.CalculateLineTotal());
    }

    public bool IsEmpty => Items.Count == 0;

    // Méthodes privées
    private CartItem GetItem(Guid productId)
    {
        return Items.FirstOrDefault(i => i.ProductId == productId)
               ?? throw new KeyNotFoundException($"Item with productId {productId} not found in cart");
    }

    private void UpdateTimestamps()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
