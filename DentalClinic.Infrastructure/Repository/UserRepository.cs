using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower(), ct);

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _ctx.Users.AddAsync(user, ct);
        await _ctx.SaveChangesAsync(ct);
    }
}