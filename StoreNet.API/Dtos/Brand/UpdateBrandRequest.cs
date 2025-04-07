using System.ComponentModel.DataAnnotations;


namespace StoreNet.API.Dtos.Brand;

public record UpdateBrandRequest(
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit faire entre 3 et 100 caractères")]
    string? Name,

    [StringLength(500, ErrorMessage = "La description ne peut dépasser 500 caractères")]
    string? Description,

    bool? IsAvailable
);
