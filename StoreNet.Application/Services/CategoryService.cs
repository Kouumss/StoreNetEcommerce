using AutoMapper;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ServiceResult<IReadOnlyList<Category>>> GetCategoriesAsync()
    {
        var categories = await _categoryRepository.ListAsync();

        if (categories is null || !categories.Any())
            return ServiceResult<IReadOnlyList<Category>>.Failure("No categories found");
        return ServiceResult<IReadOnlyList<Category>>.Success(categories);
    }

    public async Task<ServiceResult<Category>> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return ServiceResult<Category>.Failure($"Category with ID {id} not found");
        return ServiceResult<Category>.Success(category);
    }

    public async Task<ServiceResult> CreateCategoryAsync(string name, string? description)
    {   
        var category = new Category(name, description);
        var result  = await _categoryRepository.AddAsync(category);
        if (result > 0)
            return ServiceResult.Success("Category created successfully");
        return ServiceResult.Failure("Failed to create category");
    }

    public async Task<ServiceResult> UpdateCategoryAsync(Guid id, string? name, string? description)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return ServiceResult.Failure($"Category with ID {id} not found");

        category.UpdateDetails(name, description);
        var result = await _categoryRepository.UpdateAsync(category);
        if (result <= 0)
            return ServiceResult.Failure("Failed to update category");
        return ServiceResult.Success("Category updated successfully");
    }

    public async Task<ServiceResult> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return ServiceResult.Failure($"Category with ID {id} not found");
        var result = await _categoryRepository.RemoveAsync(category);
        if (result <= 0)
            return ServiceResult.Failure("Failed to delete category");
        return new ServiceResult(true, "Category deleted successfully");
    }
}
