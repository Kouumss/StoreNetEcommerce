namespace StoreNet.Domain.Filters.Cart;

    public record CartFilter(
        string? UserId,             // Filtrer par l'utilisateur (par exemple, l'ID de l'utilisateur connecté)
        List<string>? Status,       // Filtrer par le statut du panier (par exemple, 'En cours', 'Terminé', etc.)
        DateTime? StartDate,        // Filtrer les paniers créés après une certaine date
        DateTime? EndDate,          // Filtrer les paniers créés avant une certaine date
        string? SortBy,             // Critère de tri (par exemple, "CreatedAt", "TotalPrice")
        int PageIndex = 1,          // Page de résultats pour la pagination
        int PageSize = 12           // Nombre d'éléments par page
    );
