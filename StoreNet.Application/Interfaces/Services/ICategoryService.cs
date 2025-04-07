using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<ServiceResult<IReadOnlyList<Category>>> GetCategoriesAsync();
    Task<ServiceResult<Category>> GetCategoryByIdAsync(Guid id);
    Task<ServiceResult> CreateCategoryAsync(string name, string? description);
    Task<ServiceResult> UpdateCategoryAsync(Guid id, string? name, string? description);
    Task<ServiceResult> DeleteCategoryAsync(Guid id);

}

