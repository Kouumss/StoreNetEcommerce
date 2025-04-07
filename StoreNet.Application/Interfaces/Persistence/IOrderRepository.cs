using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Order;

namespace StoreNet.Application.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderFilter filter);
    Task<Order?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Order order);
    Task<int> UpdateAsync(Order order);
    Task<int> DeleteAsync(Order order);
    Task<bool> ExistsAsync(Guid customerId, DateTime orderDate);
}