using DentalClinic.Domain.Entities;

namespace DentalClinic.Application.Contracts.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, CancellationToken ct = default);
    Task AddAsync(Appointment appointment, CancellationToken ct = default);
    Task UpdateAsync(Appointment appointment, CancellationToken ct = default);
}