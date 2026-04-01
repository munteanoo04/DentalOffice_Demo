using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreatePatientCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth
) : IRequest<int>;