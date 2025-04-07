using Microsoft.AspNetCore.Mvc;
using StoreNet.Domain.Entities;
using StoreNet.Application.Interfaces.Services;
using StoreNet.API.Dtos.Category;

namespace StoreNet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    // Récupérer toutes les catégories
    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyCollection<CategoryResponse>>> GetCategories()
    {
        var categories = await categoryService.GetCategoriesAsync();

        if (categories?.Data is null || categories.Data.Count == 0)
            return NotFound(categories);

        return Ok(categories);
    }

    // Récupérer une catégorie par ID
    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid ID");

        var category = await categoryService.GetCategoryByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    // Créer une nouvelle catégorie
    [HttpPost("Create")]
    public async Task<ActionResult> CreateCategory(CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await categoryService.CreateCategoryAsync(request.Name, request.Description);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // Mettre à jour une catégorie
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await categoryService.UpdateCategoryAsync(id, request.Name, request.Description);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    // Supprimer une catégorie
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid ID");

        var result = await categoryService.DeleteCategoryAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }
}
