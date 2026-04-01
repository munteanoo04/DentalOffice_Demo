// Contracts/DTOs/ProcedureDto.cs
namespace DentalClinic.Application.Contracts.DTOs;

public record ProcedureDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int DurationMinutes,
    string Category,
    bool RequiresAnesthesia,
    bool IsActive
);