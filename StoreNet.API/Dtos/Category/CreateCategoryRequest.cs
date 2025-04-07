using System.ComponentModel.DataAnnotations;


namespace StoreNet.API.Dtos.Category;

public record CreateCategoryRequest(
    [Required][StringLength(100)] string Name,
    [Required] string? Description
);
