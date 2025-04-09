namespace StoreNet.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ICartRepository CartRepository { get; }
    IProductRepository ProductRepository { get; }
    // Ajoutez d'autres repositories ici...

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    void LogEntityStates();
}
