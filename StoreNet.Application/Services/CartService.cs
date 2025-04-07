using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;


namespace StoreNet.Application.Services;

public class CartService(
    ICartRepository cartRepository,
    IProductRepository productRepository) : ICartService
{
    public async Task<ServiceResult<Cart>> GetCartByUserIdAsync(Guid userId)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart is null)
            return ServiceResult<Cart>.Failure("Cart not found");
        return ServiceResult<Cart>.Success(cart);
    }

    public async Task<ServiceResult> AddItemAsync(Guid userId, AddCartItemDto command)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if (product is null) 
            return  ServiceResult.Failure("Product not found");

        if (product.StockQuantity < command.Quantity) 
            return  ServiceResult.Failure("Insufficient stock");

        var cart = await cartRepository.GetByUserIdAsync(userId) ?? Cart.Create(userId);

        cart.AddItem(product.Id, command.Quantity, product.Price, product.DiscountPercent);

        var result = cart.Id == Guid.Empty
            ? await cartRepository.AddAsync(cart)
            : await cartRepository.UpdateAsync(cart);

        if (result > 0)
            return ServiceResult.Success("Item added to cart");
        else
            return ServiceResult.Failure("Failed to update cart");
    }

    public async Task<ServiceResult> UpdateItemAsync(Guid userId, UpdateCartItemDto command)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart is null) 
            return ServiceResult.Failure("Cart not found");
        var product = await productRepository.GetByIdAsync(command.ProductId);
        if (product is null) 
            return ServiceResult.Failure("Product not found");
        if (product.StockQuantity < command.NewQuantity)
            return ServiceResult.Failure("Insufficient stock");

        try
        {
            cart.UpdateItemQuantity(command.ProductId, command.NewQuantity);
            var result = await cartRepository.UpdateAsync(cart);

            if(result > 0)
               return ServiceResult.Success("Item updated in cart");
            else
               return ServiceResult.Failure("Failed to update cart");
        }
        catch (KeyNotFoundException ex)
        {
            return new ServiceResult(false, ex.Message);
        }
    }

    public async Task<ServiceResult> RemoveItemAsync(Guid userId, Guid productId)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart is null) 
            return ServiceResult.Failure("Cart not found");

        try
        {
            cart.RemoveItem(productId);
            var result = await cartRepository.UpdateAsync(cart);

            if(result > 0)
                return ServiceResult.Success("Item removed from cart");
            else
                return ServiceResult.Failure("Failed to update cart");
        }
        catch (KeyNotFoundException ex)
        {
            return new ServiceResult(false, ex.Message);
        }
    }

    public async Task<ServiceResult> ClearCartAsync(Guid userId)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart is null || cart.IsEmpty)
            return ServiceResult.Failure("Cart not found or already empty");

        cart.Clear();
        var result = await cartRepository.UpdateAsync(cart);

       if (result > 0)
            return ServiceResult.Success("Cart cleared");
        else
            return ServiceResult.Failure("Failed to clear cart");
    }

    public async Task<ServiceResult<decimal>> CalculateCartTotalAsync(Guid userId)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart is null)
            return ServiceResult<decimal>.Failure("Cart not found");

        return ServiceResult<decimal>.Success(cart.CalculateTotal());
    }
}