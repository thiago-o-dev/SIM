using Sim.Domain.Abstractions;
using Sim.Infrastructure.Data;

namespace Sim.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SystemDbContext _context;
    public IUserRepository Users { get; }

    public UnitOfWork(
        SystemDbContext context,
        IUserRepository Users)
    {
        _context = context;
        Users = Users;
    }

    public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;
    public void Dispose() => _context.Dispose();
}
