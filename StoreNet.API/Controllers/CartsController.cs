using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Cart;
using StoreNet.API.Dtos.Cart.CartItems;
using StoreNet.Application.Dtos.Carts.CartItems;
using StoreNet.Application.Interfaces.Services;

namespace StoreNet.API.Controllers;

[ApiController]
[Route("api/carts")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartsController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet("{userId}", Name = "GetUserCart")]
    public async Task<ActionResult<CartResponse>> GetUserCart(Guid userId)
    {
        var result = await _cartService.GetCartByUserIdAsync(userId);
        return result.IsSuccess
            ? Ok(_mapper.Map<CartResponse>(result.Data))
            : NotFound(result.Message);
    }

    [HttpPost("create", Name = "CreateCart")]
    public async Task<ActionResult<CartResponse>> CreateCart([FromBody] CreateCartRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cartService.CreateCartAsync(request.UserId);
        return result.IsSuccess
            ? CreatedAtRoute("GetUserCart", new { userId = request.UserId }, _mapper.Map<CartResponse>(result.Data))
            : BadRequest(result.Message);
    }

    [HttpPost("{userId}/items", Name = "AddItemToCart")]
    public async Task<IActionResult> AddItem(Guid userId, [FromBody] AddCartItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = new AddCartItemDto(userId, request.ProductId, request.Quantity);
        var result = await _cartService.AddItemAsync(dto);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Message);
    }

    [HttpPut("{userId}/items/{itemId}/quantity", Name = "UpdateItemQuantity")]
    public async Task<IActionResult> UpdateItemQuantity(Guid userId, Guid productId, [FromBody] UpdateCartItemQuantityRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = new UpdateCartItemDto(userId, productId, request.NewQuantity);
        var result = await _cartService.UpdateItemQuantityAsync(dto);

        return result.IsSuccess ? Ok() : BadRequest(result.Message);
    }

    [HttpDelete("{userId}/items/{itemId}", Name = "RemoveCartItem")]
    public async Task<IActionResult> RemoveItem(Guid userId, Guid itemId)
    {
        var result = await _cartService.RemoveItemAsync(userId, itemId);
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }

    [HttpDelete("{userId}/clear", Name = "ClearCart")]
    public async Task<IActionResult> ClearCart(Guid userId)
    {
        var result = await _cartService.ClearCartAsync(userId);
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }

    [HttpGet("{userId}/total", Name = "CalculateCartTotal")]
    public async Task<ActionResult<decimal>> CalculateTotal(Guid userId)
    {
        var result = await _cartService.CalculateTotalAsync(userId);
        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }

    [HttpPost("{userId}/merge", Name = "MergeCarts")]
    public async Task<IActionResult> MergeCarts(Guid userId, [FromBody] MergeCartsRequest request)
    {
        var result = await _cartService.MergeCartsAsync(userId, request.TempCartId);
        return result.IsSuccess ? Ok() : BadRequest(result.Message);
    }

    [HttpGet("{userId}/item-count", Name = "GetItemCount")]
    public async Task<ActionResult<int>> GetItemCount(Guid userId)
    {
        var result = await _cartService.GetItemCountAsync(userId);
        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }
}
