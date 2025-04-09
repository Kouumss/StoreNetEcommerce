using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;
using AutoMapper;
using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Application.Dtos.Carts;
using Microsoft.EntityFrameworkCore;

namespace StoreNet.Application.Services;

public class CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper) : ICartService
{
    public async Task<ServiceResult<CartDto>> GetCartByUserIdAsync(Guid userId)
    {
        try
        {
            var cart = await cartRepository.GetByUserIdAsync(userId);
            if (cart is null)
                return ServiceResult<CartDto>.Failure("Cart not found");

            var cartDto = mapper.Map<CartDto>(cart);

            return ServiceResult<CartDto>.Success(cartDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<CartDto>.Failure($"Error retrieving cart: {ex.Message}");
        }
    }

    public async Task<ServiceResult<CartDto>> GetCartByIdAsync(Guid cartId)
    {
        try
        {
            var cart = await cartRepository.GetByIdAsync(cartId);
            if (cart is null)
                return ServiceResult<CartDto>.Failure("Cart not found");
            var cartDto = mapper.Map<CartDto>(cart);
            return ServiceResult<CartDto>.Success(cartDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<CartDto>.Failure($"Error retrieving cart: {ex.Message}");
        }
    }


    public async Task<ServiceResult<CartDto>> CreateCartAsync(Guid userId)
    {
        try
        {
            var existingCart = await cartRepository.GetByUserIdAsync(userId);
            if (existingCart != null)
                return ServiceResult<CartDto>.Failure("User already has a cart");

            var newCart = Cart.Create(userId);
            await cartRepository.AddAsync(newCart);

            return ServiceResult<CartDto>.Success(mapper.Map<CartDto>(newCart));
        }
        catch (Exception ex)
        {
            return ServiceResult<CartDto>.Failure($"Error creating cart: {ex.Message}");
        }
    }

    public async Task<ServiceResult> AddItemAsync(AddCartItemDto dto)
    {
        try
        {
            var cart = await cartRepository.GetByUserIdAsync(dto.UserId);
            if (cart is null)
                return ServiceResult.Failure("Cart not found");

            var product = await productRepository.GetByIdAsync(dto.ProductId);
            if (product is null)
                return ServiceResult.Failure("Product not found");

            var newItem = new CartItem(product.Id, dto.Quantity, product.Price);
            cart.Items.Add(newItem);

            await cartRepository.SaveChangesAsync(); // Utilisez SaveChangesAsync au lieu de UpdateAsync
            return ServiceResult.Success("Item added successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return ServiceResult.Failure($"Concurrency conflict: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ServiceResult.Failure($"Error adding item: {ex.Message}");
        }
    }
    //public async Task<ServiceResult> AddItemAsync(AddCartItemDto dto)
    //{
    //    try
    //    {
    //        var cart = await cartRepository.GetByUserIdAsync(dto.UserId);

    //        if (cart is null)
    //            return ServiceResult.Failure("Cart not found");

    //        var product = await productRepository.GetByIdAsync(dto.ProductId);
    //        if (product is null)
    //            return ServiceResult.Failure("Product not found");

    //        var newItem = new CartItem(product.Id, dto.Quantity, product.Price);
    //        cart.Items.Add(newItem);

    //        await cartRepository.UpdateAsync(cart);
    //        return ServiceResult.Success("Item added successfully");
    //    }
    //    catch (DbUpdateConcurrencyException ex)
    //    {
    //        return ServiceResult.Failure($"Concurrency conflict: {ex.Message}");
    //    }
    //    catch (Exception ex)
    //    {
    //        return ServiceResult.Failure($"Error adding item: {ex.Message}");
    //    }
    //}

    public async Task<ServiceResult> UpdateItemQuantityAsync(UpdateCartItemDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> RemoveItemAsync(Guid userId, Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> ClearCartAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<decimal>> CalculateTotalAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> MergeCartsAsync(Guid targetUserId, Guid sourceCartId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<int>> GetItemCountAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}