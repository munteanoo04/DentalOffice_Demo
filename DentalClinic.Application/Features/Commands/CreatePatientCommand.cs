// Features/Commands/CreatePatientCommand.cs
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreatePatientCommand(
    string Name,
    string FirstName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth
) : IRequest<Guid>;