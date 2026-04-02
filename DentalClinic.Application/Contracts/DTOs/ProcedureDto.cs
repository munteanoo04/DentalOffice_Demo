namespace DentalClinic.Application.Contracts.DTOs;

public record ProcedureDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int DurationMinutes,
    string Category,
    bool RequiresAnesthesia,
    bool IsActive
);