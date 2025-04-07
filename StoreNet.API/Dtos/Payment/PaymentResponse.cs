namespace StoreNet.API.Dtos.Payment;

public record PaymentResponse(
    string GatewayTransactionId,
    bool IsSuccess,
    string ErrorMessage = null!
);
