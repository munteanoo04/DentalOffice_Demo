// Features/Commands/CancelAppointmentCommand.cs
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CancelAppointmentCommand(Guid AppointmentId) : IRequest;