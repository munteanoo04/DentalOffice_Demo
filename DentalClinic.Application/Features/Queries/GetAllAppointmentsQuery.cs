using DentalClinic.Application.Contracts.DTOs;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public record GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>;