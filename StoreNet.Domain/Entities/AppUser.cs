using Microsoft.AspNetCore.Identity;

namespace StoreNet.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{   
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsAvailable { get; protected set; } = true;
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void SetAvailability(bool availability) => IsAvailable = availability;
}
