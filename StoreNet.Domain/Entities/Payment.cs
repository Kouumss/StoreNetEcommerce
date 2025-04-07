using System;
using StoreNet.Domain.Entities;

namespace StoreNet.Domain.Entities;


public class Payment : BaseEntity
{
    public string PaymentId { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime Date { get; set; }

    public void MarkAsPaid() => Status = PaymentStatus.Completed;
    public void MarkAsFailed() => Status = PaymentStatus.Failed;
}
