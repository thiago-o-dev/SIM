using Sim.Domain.Abstractions;
using Sim.Domain.Entities;
using Sim.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Sim.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SystemDbContext _context;

    public UserRepository(SystemDbContext context) => _context = context;

    public async Task AddAsync(User User) => await _context.Users.AddAsync(User);

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
