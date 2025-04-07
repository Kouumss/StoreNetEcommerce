using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Authentication;

public record LoginRequest(
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    [StringLength(100, ErrorMessage = "L'email ne peut dépasser 100 caractères")]
    string Email,

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Le mot de passe doit faire entre 8 et 50 caractères")]
    [DataType(DataType.Password)]
    string Password
);
// Inputs


