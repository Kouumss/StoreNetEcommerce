namespace StoreNet.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? TrackingNumber { get; private set; }
    public string? Notes { get; private set; }
    public DateTime? ShippedDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();





    // Méthode de création
    public static Order Create(
        Guid userId,
        string shippingAddress,
        string paymentMethod,
        string? notes = null)
    {
        return new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress,
            PaymentMethod = paymentMethod,
            Notes = notes,
            TotalAmount = 0
        };
    }

    // Méthodes pour les statuts
    public void Process()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException($"Cannot process order in {Status} status");

        Status = OrderStatus.Processing;
    }

    public void MarkAsShipped(string trackingNumber)
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException($"Cannot ship order in {Status} status");

        Status = OrderStatus.Shipped;
        TrackingNumber = trackingNumber;
        ShippedDate = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException($"Cannot deliver order in {Status} status");

        Status = OrderStatus.Delivered;
        DeliveredDate = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException($"Cannot cancel order in {Status} status");

        Status = OrderStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != OrderStatus.Delivered)
            throw new InvalidOperationException($"Cannot complete order in {Status} status");

        Status = OrderStatus.Completed;
    }

    // Méthode pour mettre à jour le statut
    public void UpdateStatus(string status, string? trackingNumber = null, string? notes = null)
    {
        if (!Enum.TryParse<OrderStatus>(status, out var newStatus))
            throw new ArgumentException("Invalid order status");

        switch (newStatus)
        {
            case OrderStatus.Processing:
                Process();
                break;
            case OrderStatus.Shipped:
                if (string.IsNullOrEmpty(trackingNumber))
                    throw new ArgumentException("Tracking number is required when shipping");
                MarkAsShipped(trackingNumber);
                break;
            case OrderStatus.Delivered:
                MarkAsDelivered();
                break;
            case OrderStatus.Cancelled:
                Cancel();
                break;
            case OrderStatus.Completed:
                Complete();
                break;
            default:
                Status = newStatus;
                break;
        }

        if (!string.IsNullOrEmpty(notes))
            Notes = notes;
    }

    // Méthodes pour gérer les articles
    public void AddOrderItem(Guid productId, int quantity, decimal unitPrice, decimal discountPercent)
    {
        var discountAmount = unitPrice * (discountPercent / 100);
        var finalPrice = unitPrice - discountAmount;

        var orderItem = new OrderItem
        {
            ProductId = productId,
            Quantity = quantity,
            Price = finalPrice
        };

        OrderItems.Add(orderItem);
        CalculateTotalAmount();
    }

    public void RemoveOrderItem(Guid productId)
    {
        var item = OrderItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            OrderItems.Remove(item);
            CalculateTotalAmount();
        }
    }

    // Calcul du montant total
    public void CalculateTotalAmount()
    {
        TotalAmount = OrderItems.Sum(item => item.Price * item.Quantity);
    }
}