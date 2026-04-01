using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using DentalClinic.Domain.Enums;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IPatientRepository _patientRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly IProcedureRepository _procedureRepo;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepo,
        IPatientRepository patientRepo,
        IDoctorRepository doctorRepo,
        IProcedureRepository procedureRepo)
    {
        _appointmentRepo = appointmentRepo;
        _patientRepo = patientRepo;
        _doctorRepo = doctorRepo;
        _procedureRepo = procedureRepo;
    }

    public async Task<int> Handle(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        _ = await _patientRepo.GetByIdAsync(cmd.PatientId, ct)
            ?? throw new Exception($"Patient {cmd.PatientId} not found.");

        _ = await _doctorRepo.GetByIdAsync(cmd.DoctorId, ct)
            ?? throw new Exception($"Doctor {cmd.DoctorId} not found.");

        _ = await _procedureRepo.GetByIdAsync(cmd.ProcedureId, ct)
            ?? throw new Exception($"Procedure {cmd.ProcedureId} not found.");

        var appointment = new Appointment
        {
            PatientId = cmd.PatientId,
            DoctorId = cmd.DoctorId,
            ProcedureId = cmd.ProcedureId,
            ScheduledAt = cmd.ScheduledAt,
            TreatmentType = cmd.TreatmentType,
            Notes = cmd.Notes,
            Status = AppointmentStatus.Scheduled
        };

        await _appointmentRepo.AddAsync(appointment, ct);
        return appointment.Id;
    }
}