using StoreNet.Application.Dtos.Brands;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Services;

public interface IBrandService
{
    Task<ServiceResult<IReadOnlyList<Brand>>> GetBrandsAsync();
    Task<ServiceResult<Brand?>> GetBrandByIdAsync(Guid id);
    Task<ServiceResult> CreateBrandAsync(CreateBrandDto command);
    Task<ServiceResult> UpdateBrandAsync(UpdateBrandDto command);
    Task<ServiceResult> DeleteBrandAsync(Guid id);
}