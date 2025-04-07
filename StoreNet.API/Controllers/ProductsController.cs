using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Product;
using StoreNet.Application.Dtos.Products;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Filters.Product;

namespace StoreNet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService, IMapper mapper) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAllProducts([FromQuery] ProductFilter filter)
    {
        ServiceResult<IReadOnlyList<ProductDto>>? result = await productService.GetAllProductsAsync(filter);

        if (result.IsSuccess)
        {
            var response = mapper.Map<IReadOnlyList<ProductResponse>>(result.Data);

            return Ok(response);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
    {
        ServiceResult<ProductDto>? result = await productService.GetProductByIdAsync(id);

        if (result.IsSuccess)
        {
            var response = mapper.Map<ProductResponse>(result.Data);
            return Ok(response);
        }
        return NotFound(result.Message);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = mapper.Map<ProductCreateDto>(request);
        var result = await productService.CreateProductAsync(dto);

        if (result.IsSuccess)
        {
            var response = mapper.Map<ProductResponse>(result.Data);
            return Ok(response);
        }

        return BadRequest(result.Message);
    }
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = mapper.Map<UpdateProductDto>((Request: request, Id: id));
        var result = await productService.UpdateProductAsync(dto);

        if (result.IsSuccess)
        {
            var response = mapper.Map<ProductResponse>(result.Data);
            return Ok(response);
        }
        return BadRequest(result.Message);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await productService.DeleteProductAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
}
