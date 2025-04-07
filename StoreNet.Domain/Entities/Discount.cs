namespace StoreNet.Domain.Entities;

public class Discount : BaseEntity
{
    public decimal Percentage { get; private set; }
    public string Description { get; private set; }

    // Default 0% discount
    public static Discount NoDiscount = new Discount(0, "No discount");

    public Discount(decimal percentage, string description)
    {
        Percentage = percentage;
        Description = description;
    }
}
