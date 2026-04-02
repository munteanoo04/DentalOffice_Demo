using DentalClinic.Application.Contracts.DTOs;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public record GetAppointmentsByPatientQuery(int PatientId) : IRequest<IEnumerable<AppointmentDto>>;