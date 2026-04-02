namespace DentalClinic.Application.Contracts.DTOs;

public record PatientDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    int Age,
    bool IsActive
);