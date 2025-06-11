using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface ITodoRepository
{
    Task<Todo?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Todo>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Todo todo, CancellationToken ct = default);
}
