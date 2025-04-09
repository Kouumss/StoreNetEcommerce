using Microsoft.AspNetCore.Mvc;
using StoreNet.Domain.Entities;
using StoreNet.Application.Interfaces.Services;
using StoreNet.API.Dtos.Brand;
using AutoMapper;
using StoreNet.Application.Dtos.Brands;
using StoreNet.Application.Dtos.Products;

namespace StoreNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService, IMapper mapper) : ControllerBase
    {
        // Récupérer toutes les marques
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await brandService.GetBrandsAsync();

            if (brands is null || brands.Data.Count == 0)
                return NotFound("No brands found");
            return Ok(brands.Data);
        }

        // Récupérer une marque par ID
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid ID");

            var brand = await brandService.GetBrandByIdAsync(id);

            if (brand is null)
                return NotFound();

            return Ok(brand);
        }

        // Créer une nouvelle marque
        [HttpPost("Create")]
        public async Task<ActionResult> CreateBrand([FromBody] CreateBrandRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createBrandCommand = mapper.Map<CreateBrandDto>(request);
            var result = await brandService.CreateBrandAsync(createBrandCommand);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        //Mettre à jour une marque

        // Mettre à jour une marque
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, UpdateBrandRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new UpdateBrandDto(
                Name: request.Name,
                Description: request.Description,
                IsAvailable: request.IsAvailable
            ) with
            { Id = id };

            var result = await brandService.UpdateBrandAsync(dto);

            return result.IsSuccess
                ? Ok(result)
                : NotFound(result);
        }
        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> UpdateBrand(Guid id, UpdateBrandRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var dto = mapper.Map<UpdateBrandDto>((Request: request, Id: id));
        //    var result = await brandService.UpdateBrandAsync(dto);

        //    if (!result.IsSuccess)
        //        return NotFound(result);

        //    return Ok(result);
        //}

        // Supprimer une marque
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid ID");

            var result = await brandService.DeleteBrandAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
    }
}
