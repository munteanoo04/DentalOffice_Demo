using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _ctx;

    public AppointmentRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Appointment?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken ct = default)
        => await _ctx.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.ScheduledAt)  // ← schimbă aici
            .ToListAsync(ct);

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken ct = default)
        => await _ctx.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.ScheduledAt)  // ← și aici
            .ToListAsync(ct);

    public async Task AddAsync(Appointment appointment, CancellationToken ct = default)
    {
        await _ctx.Appointments.AddAsync(appointment, ct);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _ctx.Appointments.Update(appointment);
        await _ctx.SaveChangesAsync(ct);
    }
}