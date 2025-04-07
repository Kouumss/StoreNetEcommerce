using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Address;

public record CreateAddressRequest(
    [Required(ErrorMessage = "Le numéro de rue est requis")]
    [StringLength(10, ErrorMessage = "Le numéro ne peut dépasser 10 caractères")]
    string StreetNumber,

    [Required(ErrorMessage = "Le nom de rue est requis")]
    [StringLength(100, ErrorMessage = "Le nom de rue ne peut dépasser 100 caractères")]
    string StreetName,

    [Required(ErrorMessage = "La ville est requise")]
    [StringLength(50, ErrorMessage = "Le nom de ville ne peut dépasser 50 caractères")]
    string City,

    [Required(ErrorMessage = "Le code postal est requis")]
    [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Format de code postal invalide")]
    string PostalCode,

    [Required(ErrorMessage = "Le pays est requis")]
    [StringLength(50, ErrorMessage = "Le nom de pays ne peut dépasser 50 caractères")]
    string Country
);
