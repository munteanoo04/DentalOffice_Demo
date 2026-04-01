// Contracts/DTOs/DoctorDto.cs
namespace DentalClinic.Application.Contracts.DTOs;

public record DoctorDto(
    Guid Id,
    string Name,
    string FirstName,
    string Email,
    string PhoneNumber,
    string Specialization,
    bool IsActive
);