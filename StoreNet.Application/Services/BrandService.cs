using AutoMapper;
using StoreNet.Application.Dtos.Brands;
using StoreNet.Application.Dtos.Products;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<ServiceResult<IReadOnlyList<Brand>>> GetBrandsAsync()
    {
        var brands = await _brandRepository.ListAsync();
        if (brands is null || !brands.Any())
            return ServiceResult<IReadOnlyList<Brand>>.Failure("No brands found");
        return ServiceResult<IReadOnlyList<Brand>>.Success(brands);
    }

    public async Task<ServiceResult<Brand?>> GetBrandByIdAsync(Guid id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        if (brand is null)
            return ServiceResult<Brand?>.Failure($"Brand with ID {id} not found");
        return ServiceResult<Brand?>.Success(brand);
    }

    public async Task<ServiceResult> CreateBrandAsync(CreateBrandDto command)
    {   
        var brand = _mapper.Map<Brand>(command);
        var result = await _brandRepository.AddAsync(brand);
        if(result > 0)
            return ServiceResult.Success("Brand created successfully");
        return ServiceResult.Failure("Failed to create brand");
    }

    public async Task<ServiceResult> UpdateBrandAsync(UpdateBrandDto dto)
    {
        var brand = await _brandRepository.GetByIdAsync(dto.Id);

        if (brand is null)
            return ServiceResult.Failure($"Brand with ID {dto.Id} not found");

        brand.UpdateDetails(dto.Name, dto.Description, dto.IsAvailable);

        int result = await _brandRepository.UpdateAsync(brand);
        if (result > 0)
            return ServiceResult.Success("Brand updated successfully");
        return ServiceResult.Failure("Failed to update brand");
    }

    public async Task<ServiceResult> DeleteBrandAsync(Guid id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        if (brand is null)
            return ServiceResult.Failure("Brand not found");

        var result = await _brandRepository.RemoveAsync(brand);
        if (result > 0)
            return ServiceResult.Success("Brand deleted successfully");
        return ServiceResult.Failure("Failed to delete brand");
    }
}
