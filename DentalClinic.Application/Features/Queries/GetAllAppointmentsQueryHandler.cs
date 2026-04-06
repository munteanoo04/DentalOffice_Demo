using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public class GetAllAppointmentsQueryHandler
    : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _repo;
    public GetAllAppointmentsQueryHandler(IAppointmentRepository repo) => _repo = repo;

    public async Task<IEnumerable<AppointmentDto>> Handle(
        GetAllAppointmentsQuery query, CancellationToken ct)
    {
        var appointments = await _repo.GetAllAsync(ct);
        return appointments.Select(a => new AppointmentDto(
            a.Id,
            a.PatientId,
            $"{a.Patient.FirstName} {a.Patient.LastName}",
            a.DoctorId,
            $"{a.Doctor.FirstName} {a.Doctor.LastName}",
            a.ProcedureId,
            a.Procedure.Name,
            a.ScheduledAt,
            a.TreatmentType,
            a.Notes,
            a.Status.ToString()));
    }
}