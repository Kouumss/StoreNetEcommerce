﻿namespace StoreNet.API.Dtos.Order.OrderItems;

public record OrderItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}