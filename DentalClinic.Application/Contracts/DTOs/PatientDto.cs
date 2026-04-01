// Contracts/DTOs/PatientDto.cs
namespace DentalClinic.Application.Contracts.DTOs;

public record PatientDto(
    Guid Id,
    string Name,
    string FirstName,
    string Email,
    string PhoneNumber,
    int Age,
    bool IsActive
);