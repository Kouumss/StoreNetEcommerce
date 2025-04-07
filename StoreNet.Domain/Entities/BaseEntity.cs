using System.ComponentModel.DataAnnotations;

namespace StoreNet.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsAvailable { get; protected set; } = true;
    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void SetAvailability(bool availability) => IsAvailable = availability;
}