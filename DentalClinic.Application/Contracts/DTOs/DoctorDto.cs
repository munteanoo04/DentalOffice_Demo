namespace DentalClinic.Application.Contracts.DTOs;

public record DoctorDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    bool IsActive
);