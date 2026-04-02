using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _ctx;
    public DoctorRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Doctor?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Doctors
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => d.Id == id, ct);

    public async Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Doctors.Where(d => d.IsActive).ToListAsync(ct);

    public async Task AddAsync(Doctor doctor, CancellationToken ct = default)
    {
        await _ctx.Doctors.AddAsync(doctor, ct);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken ct = default)
    {
        _ctx.Doctors.Update(doctor);
        await _ctx.SaveChangesAsync(ct);
    }
}