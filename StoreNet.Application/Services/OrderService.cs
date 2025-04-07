using AutoMapper;
using StoreNet.Application.Dtos.Orders;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Order;


namespace StoreNet.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUserRepository userRepository,
    IMapper mapper) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    public async Task<ServiceResult<IReadOnlyList<OrderDto>>> GetAllOrdersdAsync(OrderFilter filter)
    {
        var orders = await _orderRepository.GetAllOrdersAsync(filter);
        var response = mapper.Map<IReadOnlyList<OrderDto>>(orders);

        return orders.Count switch
        {
            0 => ServiceResult<IReadOnlyList<OrderDto>>.Success(response, "No orders found matching criteria"),
            _ => ServiceResult<IReadOnlyList<OrderDto>>.Success(response)
        };
    }

    public async Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
            return ServiceResult<OrderDto>.Failure($"Order with ID {id} not found");
        var orderMapped = mapper.Map<OrderDto>(order);
        return ServiceResult<OrderDto>.Success(orderMapped);
    }

    public async Task<ServiceResult> CreateOrderAsync(CreateOrderDto dto)
    {
        var user = await _userRepository.GetUserByIdAsync(dto.UserId);
        if (user is null)
            return ServiceResult.Failure($"User with ID {dto.UserId} not found");

        foreach (var item in dto.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product is null)
                return ServiceResult.Failure($"Product with ID {item.ProductId} not found");

            if (product.StockQuantity < item.Quantity)
                return ServiceResult.Failure($"Insufficient stock for product {product.Name}");
        }

        var order = Order.Create(
            dto.UserId,
            dto.ShippingAddress,
            dto.PaymentMethod,
            dto.Notes);

        foreach (var item in dto.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            order.AddOrderItem(product!.Id, item.Quantity, product.Price, product.DiscountPercent);
        }

        int result = await _orderRepository.AddAsync(order);

        if (result > 0)
        {
            foreach (var item in dto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                product!.ReduceStock(item.Quantity);
                await _productRepository.UpdateAsync(product);
            }
            return ServiceResult.Success("Order created successfully");
        }
        return ServiceResult.Failure("Failed to create order");
    }

    public async Task<ServiceResult> UpdateOrderAsync(Guid id, UpdateOrderDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
            return ServiceResult.Failure($"Order with ID {id} not found");

        order.UpdateStatus(dto.Status, dto.TrackingNumber, dto.Notes);

        if (dto.Status == "Shipped" && dto.TrackingNumber != null)
            order.MarkAsShipped(dto.TrackingNumber);

        if (dto.Status == "Delivered" && dto.TrackingNumber != null)
            order.MarkAsDelivered();
        int result = await _orderRepository.UpdateAsync(order);

        if (result > 0)
            return ServiceResult.Success("Order updated successfully");
        return ServiceResult.Failure("Failed to update order");
    }

    public async Task<ServiceResult> CancelOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
            return ServiceResult.Failure("Order not found");
        order.Cancel();
        foreach (var item in order.OrderItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                product.Restock(item.Quantity);
                await _productRepository.UpdateAsync(product);
            }
        }
        int result = await _orderRepository.UpdateAsync(order);
        if (result > 0)
            return ServiceResult.Success("Order cancelled successfully");
        return ServiceResult.Failure("Failed to cancel order");
    }

    public async Task<ServiceResult> ProcessOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
            return ServiceResult.Failure($"Order with {id} not found");

        order.Process();

        int result = await _orderRepository.UpdateAsync(order);
        if (result > 0)
            return ServiceResult.Success("Order processed successfully");
        return ServiceResult.Failure("Failed to process order");
    }

    public async Task<ServiceResult> CompleteOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
            return ServiceResult.Failure($"Order with {id} not found");

        order.Complete();

        int result = await _orderRepository.UpdateAsync(order);
        if (result > 0)
            return ServiceResult.Success("Order completed successfully");
        return ServiceResult.Failure("Failed to complete order");
    }


}
