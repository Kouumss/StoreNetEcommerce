using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Cart;

public record MergeCartsRequest(
    [Required] Guid TempCartId);