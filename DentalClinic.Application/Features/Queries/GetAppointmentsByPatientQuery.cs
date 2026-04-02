using DentalClinic.Application.Contracts.DTOs;
using MediatR;

public record GetAppointmentsByPatientQuery(int PatientId) : IRequest<IEnumerable<AppointmentDto>>;