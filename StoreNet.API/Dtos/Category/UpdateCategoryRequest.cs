using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Category;

public record UpdateCategoryRequest(
    [StringLength(100, MinimumLength = 3)] string? Name,
    [StringLength(500, MinimumLength = 3)] string? Description,
    bool? IsAvailable);