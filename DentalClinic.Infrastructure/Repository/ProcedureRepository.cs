using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repository;

public class ProcedureRepository : IProcedureRepository
{
    private readonly AppDbContext _ctx;
    public ProcedureRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Procedure?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Procedures.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IEnumerable<Procedure>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Procedures.Where(p => p.IsActive).ToListAsync(ct);

    public async Task AddAsync(Procedure procedure, CancellationToken ct = default)
    {
        await _ctx.Procedures.AddAsync(procedure, ct);
        await _ctx.SaveChangesAsync(ct);
    }
}