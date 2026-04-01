using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CancelAppointmentCommand(int AppointmentId) : IRequest;