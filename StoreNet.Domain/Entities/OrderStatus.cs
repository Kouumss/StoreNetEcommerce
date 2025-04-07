namespace StoreNet.Domain.Entities;

public enum OrderStatus
{
    Pending,    // Commande créée mais non traitée
    Processing, // Commande en cours de préparation
    Shipped,    // Commande expédiée
    Delivered,  // Commande livrée
    Completed,  // Commande terminée (après livraison)
    Cancelled   // Commande annulée
}