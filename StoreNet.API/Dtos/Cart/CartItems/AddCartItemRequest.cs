using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Cart.CartItems;

public record AddCartItemRequest(
    [Required(ErrorMessage = "L'ID du produit est requis")]
    Guid ProductId,

    [Required(ErrorMessage = "La quantité est requise")]
    [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être au moins 1")]
    int Quantity
);
