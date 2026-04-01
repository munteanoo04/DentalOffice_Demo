// Features/Commands/CreateDoctorCommand.cs
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreateDoctorCommand(
    string Name,
    string FirstName,
    string Email,
    string PhoneNumber,
    string Specialization
) : IRequest<Guid>;