using DentalClinic.Domain.Entities;

namespace DentalClinic.Application.Contracts.Interfaces;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Doctor>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Doctor doctor, CancellationToken ct = default);
    Task UpdateAsync(Doctor doctor, CancellationToken ct = default);
}