namespace StoreNet.Domain.Filters.User;

public record UserFilter(
    List<string>? Roles,             // Liste des rôles pour filtrer par rôle utilisateur
    string? SearchTerm,              // Terme de recherche pour le nom, prénom, email, etc.
    string? SortBy,                  // Critère de tri (ex : "Name", "Email", "DateCreated")
    bool? IsActive,                  // Filtrer les utilisateurs actifs/inactifs
    int PageIndex = 1,               // Index de la page (par défaut à 1)
    int PageSize = 12                // Nombre d'éléments par page (par défaut à 12)
);