using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Cart;
using StoreNet.API.Dtos.Cart.CartItems;
using StoreNet.API.Dtos.Service;
using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Application.Interfaces.Services;

namespace StoreNet.API.Controllers;


[Route("api/carts")]
[ApiController]
public class CartsController(ICartService cartService, IMapper mapper) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<CartResponse>> GetCart(Guid userId)
    {
        return Ok(await cartService.GetCartByUserIdAsync(userId));
    }

    [HttpPost("user/{userId}/items")]
    public async Task<ActionResult<ServiceResponse>> AddItem(Guid userId, AddCartItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createCartItemCommand = mapper.Map<AddCartItemDto>(request);
        var result = await cartService.AddItemAsync(userId, createCartItemCommand);
        if(!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetCart), new { userId }, result);
    }

    [HttpPut("user/{userId}/items")]
    public async Task<ActionResult<ServiceResponse>> UpdateItem(Guid userId, UpdateCartItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var updateCartItemCommand = mapper.Map<UpdateCartItemDto>(request);
        var result = await cartService.UpdateItemAsync(userId, updateCartItemCommand);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("user/{userId}/items/{productId}")]
    public async Task<ActionResult<ServiceResponse>> RemoveItem(Guid userId, Guid productId)
    {
        var result = await cartService.RemoveItemAsync(userId, productId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("user/{userId}/clear")]
    public async Task<ActionResult<ServiceResponse>> ClearCart(Guid userId)
    {
        var result = await cartService.ClearCartAsync(userId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("user/{userId}/total")]
    public async Task<ActionResult<decimal>> GetTotal(Guid userId)
    {
        return Ok(await cartService.CalculateCartTotalAsync(userId));
    }
}
