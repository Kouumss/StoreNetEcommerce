using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Cart.CartItems;

public record UpdateCartItemRequest(
    [Required(ErrorMessage = "L'ID du produit est requis")]
    Guid ProductId,

    [Required(ErrorMessage = "La quantité est requise")]
    [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative")]
    int NewQuantity
);