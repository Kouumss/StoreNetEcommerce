namespace StoreNet.API.Dtos.Payment;

public record CheckoutSessionRequest(
    Guid OrderId,
    decimal Amount,
    string SuccessUrl,
    string CancelUrl
);
