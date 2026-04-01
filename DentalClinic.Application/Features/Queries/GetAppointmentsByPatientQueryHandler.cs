// Features/Queries/GetAppointmentsByPatientQueryHandler.cs
using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public class GetAppointmentsByPatientQueryHandler
    : IRequestHandler<GetAppointmentsByPatientQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _repo;
    public GetAppointmentsByPatientQueryHandler(IAppointmentRepository repo) => _repo = repo;

    public async Task<IEnumerable<AppointmentDto>> Handle(
        GetAppointmentsByPatientQuery query, CancellationToken ct)
    {
        var appointments = await _repo.GetByPatientIdAsync(query.PatientId, ct);
        return appointments.Select(a => new AppointmentDto(
            a.Id,
            a.PatientId, a.Patient.Name + " " + a.Patient.FirstName,
            a.DoctorId, a.Doctor.Name + " " + a.Doctor.FirstName,
            a.ProcedureId, a.Procedure.Name,
            a.ScheduledAt, a.TreatmentType, a.Notes,
            a.Status.ToString()));
    }
}