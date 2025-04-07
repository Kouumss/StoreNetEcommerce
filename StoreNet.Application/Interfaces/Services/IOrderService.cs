using StoreNet.Application.Dtos.Orders;
using StoreNet.Domain.Filters.Order;

namespace StoreNet.Application.Interfaces.Services;

public interface IOrderService
{
    Task<ServiceResult<IReadOnlyList<OrderDto>>> GetAllOrdersdAsync(OrderFilter filter);
    Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid id);
    Task<ServiceResult> CreateOrderAsync(CreateOrderDto dto);
    Task<ServiceResult> UpdateOrderAsync(Guid id, UpdateOrderDto dto);
    Task<ServiceResult> CancelOrderAsync(Guid id);
    Task<ServiceResult> ProcessOrderAsync(Guid id);
    Task<ServiceResult> CompleteOrderAsync(Guid id);
}
