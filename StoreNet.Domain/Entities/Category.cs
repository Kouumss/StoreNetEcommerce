using System.Text.Json.Serialization;

namespace StoreNet.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();


    public Category() { }


    [JsonConstructor]
    public Category(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public void UpdateDetails(
     string? name = null,
     string? description = null,
     bool? isAvailable = null)
    {
        if (name is not null) Name = name;
        if (description is not null) Description = description;
        if (isAvailable is not null) SetAvailability(isAvailable.Value);

        MarkAsUpdated();
    }
}
