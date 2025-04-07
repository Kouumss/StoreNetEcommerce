using System.ComponentModel.DataAnnotations;

namespace StoreNet.Application.Dtos.Category;

public record CreateCategoryDto(
    [Required][StringLength(100)] string Name,
    [Required] string? Description
);
