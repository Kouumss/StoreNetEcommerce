namespace StoreNet.Application.Dtos.Carts;

public record CartSummaryDto(
    decimal SubTotal,
    decimal DiscountAmount,
    decimal TaxAmount);