using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Order;

public class UpdateOrderRequest
{
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? TrackingNumber { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}
