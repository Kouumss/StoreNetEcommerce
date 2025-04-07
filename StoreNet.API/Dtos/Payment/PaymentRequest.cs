namespace StoreNet.API.Dtos.Payment;

public record PaymentRequest(
    Guid OrderId,
    decimal Amount,
    string Currency,
    string Description
);
