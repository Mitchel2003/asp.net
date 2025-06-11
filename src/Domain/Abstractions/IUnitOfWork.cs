namespace Domain.Abstractions;

public interface IUnitOfWork
{
    /// <summary>
    /// Persist changes to the underlying data store.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
