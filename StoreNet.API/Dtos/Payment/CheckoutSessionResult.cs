namespace StoreNet.API.Dtos.Payment;

public record CheckoutSessionResult(
    string SessionUrl,
    string SessionId
);