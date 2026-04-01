using DentalClinic.Domain.Entities;

namespace DentalClinic.Application.Contracts.Interfaces;

public interface IProcedureRepository
{
    Task<Procedure?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Procedure>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Procedure procedure, CancellationToken ct = default);
}