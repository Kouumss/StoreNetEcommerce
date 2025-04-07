using System.ComponentModel.DataAnnotations;

namespace StoreNet.Application.Dtos.Category;

public record UpdateCategoryDto(
    [StringLength(100, MinimumLength = 3)] string? Name,
    [StringLength(500, MinimumLength = 3)] string? Description,
    bool? IsAvailable);