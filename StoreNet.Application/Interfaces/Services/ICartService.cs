using StoreNet.Application.Dtos.Carts;
using StoreNet.Application.Dtos.Carts.CartItems;

namespace StoreNet.Application.Interfaces.Services;

public interface ICartService
{
    Task<ServiceResult<CartDto>> GetCartByUserIdAsync(Guid userId);
    Task<ServiceResult<CartDto>> CreateCartAsync(Guid userId);
    Task<ServiceResult> AddItemAsync(AddCartItemDto dto);
    Task<ServiceResult> UpdateItemQuantityAsync(UpdateCartItemDto dto);
    Task<ServiceResult> RemoveItemAsync(Guid userId, Guid productId);
    Task<ServiceResult> ClearCartAsync(Guid userId);
    Task<ServiceResult<decimal>> CalculateTotalAsync(Guid userId);
    Task<ServiceResult> MergeCartsAsync(Guid targetUserId, Guid sourceCartId);
    Task<ServiceResult<int>> GetItemCountAsync(Guid userId);
}