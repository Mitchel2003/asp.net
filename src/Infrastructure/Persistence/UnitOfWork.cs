using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;
    public UnitOfWork(AppDbContext ctx) => _ctx = ctx;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Optionally, publish domain events here before saving.
        return await _ctx.SaveChangesAsync(cancellationToken);
    }
}
