using DentalClinic.Domain.Entities;

namespace DentalClinic.Application.Contracts.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Patient?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IEnumerable<Patient>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Patient patient, CancellationToken ct = default);
    Task UpdateAsync(Patient patient, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}