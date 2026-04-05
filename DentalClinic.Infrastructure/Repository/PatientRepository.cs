using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _ctx;
    public PatientRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Patient?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Patients
            .Include(p => p.Appointments)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _ctx.Patients.FirstOrDefaultAsync(p => p.Email == email, ct);

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Patients.Where(p => p.IsActive).ToListAsync(ct);

    public async Task AddAsync(Patient patient, CancellationToken ct = default)
    {
        await _ctx.Patients.AddAsync(patient, ct);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct = default)
    {
        _ctx.Patients.Update(patient);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var patient = await GetByIdAsync(id, ct);
        if (patient is not null)
        {
            patient.Deactivate(); 
            await _ctx.SaveChangesAsync(ct);
        }
    }
}