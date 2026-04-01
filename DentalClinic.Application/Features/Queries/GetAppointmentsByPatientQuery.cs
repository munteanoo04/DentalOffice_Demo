// Features/Queries/GetAppointmentsByPatientQuery.cs
using DentalClinic.Application.Contracts.DTOs;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public record GetAppointmentsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<AppointmentDto>>;