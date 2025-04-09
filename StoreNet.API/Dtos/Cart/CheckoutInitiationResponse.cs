namespace StoreNet.API.Dtos.Cart;

public record CheckoutInitiationResponse(
    Guid CartId,
    decimal TotalAmount,
    int ItemCount);