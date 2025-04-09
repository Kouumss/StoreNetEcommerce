namespace StoreNet.Domain.Entities;


public class Cart : BaseEntity
{
    // Propriétés
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; }
    public ICollection<CartItem> Items { get; private set; }


    // Factory method
    public static Cart Create(Guid userId)
    {
        return new Cart
        {
            UserId = userId,
            Items = new List<CartItem>()
        };
    }
}