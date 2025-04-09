using System.ComponentModel.DataAnnotations;

namespace StoreNet.Domain.Entities;

public class Product : BaseEntity
{
    private string _name = string.Empty;
    private string _description = string.Empty;
    private decimal _price;
    private int _stockQuantity;
    private string _imageUrl = string.Empty;
    private int _discountPercent;

  


    public string Name
    {
        get => _name;
        private set => _name = ValidateName(value);
    }

    public string Description
    {
        get => _description;
        private set => _description = ValidateDescription(value);
    }

    public decimal Price
    {
        get => _price;
        private set => _price = ValidatePrice(value);
    }

    public int StockQuantity
    {
        get => _stockQuantity;
        private set => _stockQuantity = ValidateStockQuantity(value);
    }

    public string ImageUrl
    {
        get => _imageUrl;
        private set => _imageUrl = ValidateImageUrl(value);
    }

    public int DiscountPercent
    {
        get => _discountPercent;
        private set => _discountPercent = ValidateDiscount(value);
    }

    public Category Category { get; set; }
    public Brand Brand { get; set; }

    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }

    public decimal PriceAfterDiscount => Price * (1 - DiscountPercent / 100m);

    public ICollection<OrderItem> OrderItems { get; set; }

    public static Product Create(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        Guid categoryId,
        Guid brandId,
        string imageUrl = "",
        int discountPercent = 0)
    {
        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stockQuantity,
            CategoryId = categoryId,
            BrandId = brandId,
            ImageUrl = imageUrl,
            DiscountPercent = discountPercent,
        };
    }

    public void ApplyDiscount(int discountPercent)
    {
        DiscountPercent = discountPercent;
        MarkAsUpdated();
    }

    public void RemoveDiscount()
    {
        DiscountPercent = 0;
        MarkAsUpdated();
    }

    public void Restock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Restock quantity must be positive.");

        StockQuantity += quantity;
        MarkAsUpdated();
    }

    // Réduire les stocks
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Reduce quantity must be positive.");

        if (StockQuantity < quantity)
            throw new ArgumentException("Insufficient stock.");

        StockQuantity -= quantity;
        MarkAsUpdated();
    }

    // Mettre à jour les détails du produit
    public void UpdateDetails(
        string? name = null,
        string? description = null,
        decimal? price = null,
        int? stockQuantity = null,
        string? imageUrl = null,
        int? discountPercent = null,
        Guid? categoryId = null,
        Guid? brandId = null,
        bool? isAvailable = null)
    {
        if (name is not null) Name = name;
        if (description is not null) Description = description;
        if (price is not null) Price = price.Value;
        if (stockQuantity is not null) StockQuantity = stockQuantity.Value;
        if (imageUrl is not null) ImageUrl = imageUrl;
        if (discountPercent is not null) DiscountPercent = discountPercent.Value;
        if (categoryId is not null) CategoryId = categoryId.Value;
        if (brandId is not null) BrandId = brandId.Value;
        if (isAvailable is not null) SetAvailability(isAvailable.Value);

        MarkAsUpdated();
    }

    // Méthodes de validation
    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length is < 3 or > 100)
            throw new ArgumentException("Name must be between 3 and 100 characters.");
        return name;
    }

    private static string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length < 10)
            throw new ArgumentException("Description must be at least 10 characters.");
        return description;
    }

    private static decimal ValidatePrice(decimal price)
    {
        if (price is < 0.01m or > 10000m)
            throw new ArgumentException("Price must be between 0.01 and 10,000.");
        return price;
    }

    private static int ValidateStockQuantity(int quantity)
    {
        if (quantity is < 0 or > 1000)
            throw new ArgumentException("Stock quantity must be between 0 and 1000.");
        return quantity;
    }

    private static string ValidateImageUrl(string url)
    {
        if (!string.IsNullOrEmpty(url) && !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new ArgumentException("Invalid URL format.");
        return url;
    }

    private static int ValidateDiscount(int discount)
    {
        if (discount is < 0 or > 100)
            throw new ArgumentException("Discount must be between 0 and 100.");
        return discount;
    }
}


