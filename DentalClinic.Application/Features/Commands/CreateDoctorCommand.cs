using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreateDoctorCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization
) : IRequest<int>;