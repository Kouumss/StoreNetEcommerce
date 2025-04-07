using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Services;

public interface ICartService
{
    Task<ServiceResult<Cart>> GetCartByUserIdAsync(Guid userId);
    Task<ServiceResult> AddItemAsync(Guid userId, AddCartItemDto command);
    Task<ServiceResult> UpdateItemAsync(Guid userId, UpdateCartItemDto command);
    Task<ServiceResult> RemoveItemAsync(Guid userId, Guid productId);
    Task<ServiceResult> ClearCartAsync(Guid userId);
    Task<ServiceResult<decimal>> CalculateCartTotalAsync(Guid userId);
}

