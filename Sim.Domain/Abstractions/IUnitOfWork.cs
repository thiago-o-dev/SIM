namespace Sim.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }

    Task<bool> CommitAsync();
}
