using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _ctx;
    public TodoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task AddAsync(Todo todo, CancellationToken ct = default)
    {
        await _ctx.AddAsync(todo, ct);
    }

    public async Task<List<Todo>> GetAllAsync(CancellationToken ct = default)
    {
        return await _ctx.Todos.AsNoTracking().ToListAsync(ct);
    }

    public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _ctx.Todos.FindAsync(new object?[] { id }, ct);
    }
}
