using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Order;
using StoreNet.Infrastructure.Data;

namespace StoreNet.Infrastructure.Persistence;


public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderFilter filter)
    {
        IQueryable<Order> query = _context.Orders
            .Include(o => o.OrderItems)
            .AsQueryable();

        // Appliquer les filtres
        if (filter.UserId.HasValue)
            query = query.Where(o => o.UserId == filter.UserId.Value);

        if (filter.Status.HasValue)
            query = query.Where(o => o.Status == filter.Status);

        if (filter.FromDate.HasValue)
            query = query.Where(o => o.OrderDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(o => o.OrderDate <= filter.ToDate.Value);

        // Compter le total avant la pagination
        int totalCount = await query.CountAsync();

        // Appliquer la pagination
        var orders = await query
            .OrderByDescending(o => o.OrderDate)
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return orders.AsReadOnly(); 
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product) 
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<int> AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid customerId, DateTime orderDate)
    {
        return await _context.Orders
            .AnyAsync(o => o.UserId == customerId && o.OrderDate.Date == orderDate.Date);
    }
}
