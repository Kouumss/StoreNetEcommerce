using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Order;
using StoreNet.Application.Dtos.Orders;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Filters.Order;

namespace StoreNet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, IMapper mapper) : ControllerBase
{

    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrders([FromQuery] OrderFilter filter)
    {
        var result = await orderService.GetAllOrdersdAsync(filter);

        if (result.IsSuccess)
        {
            var response = mapper.Map<IReadOnlyList<OrderResponse>>(result.Data);

            return Ok(response);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var result = await orderService.GetOrderByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var orderCommand = mapper.Map<CreateOrderDto>(request);
        var result = await orderService.CreateOrderAsync(orderCommand);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var updateOrderCommand = mapper.Map<UpdateOrderDto>(request);
        var result = await orderService.UpdateOrderAsync(id, updateOrderCommand);

        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var result = await orderService.CancelOrderAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("{id}/process")]
    public async Task<IActionResult> ProcessOrder(Guid id)
    {
        var result = await orderService.ProcessOrderAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        var result = await orderService.CompleteOrderAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
