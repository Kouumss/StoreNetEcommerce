using System.ComponentModel.DataAnnotations;


namespace StoreNet.API.Dtos.Brand;


public record CreateBrandRequest(
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit faire entre 3 et 100 caractères")]
    string Name,

    [Required(ErrorMessage = "La description est obligatoire")]
    [StringLength(500, ErrorMessage = "La description ne peut dépasser 500 caractères")]
    string Description
);
