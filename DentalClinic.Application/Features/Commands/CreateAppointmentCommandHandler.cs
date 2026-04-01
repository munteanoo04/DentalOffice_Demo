// Features/Commands/CreateAppointmentCommandHandler.cs
using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IPatientRepository _patientRepo;
    private readonly IDoctorRepository _doctorRepo;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepo,
        IPatientRepository patientRepo,
        IDoctorRepository doctorRepo)
    {
        _appointmentRepo = appointmentRepo;
        _patientRepo = patientRepo;
        _doctorRepo = doctorRepo;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        _ = await _patientRepo.GetByIdAsync(cmd.PatientId, ct)
            ?? throw new Exception($"Patient {cmd.PatientId} not found.");

        _ = await _doctorRepo.GetByIdAsync(cmd.DoctorId, ct)
            ?? throw new Exception($"Doctor {cmd.DoctorId} not found.");

        var appointment = Appointment.Create(
            cmd.PatientId, cmd.DoctorId, cmd.ProcedureId,
            cmd.ScheduledAt, cmd.TreatmentType, cmd.Notes);

        await _appointmentRepo.AddAsync(appointment, ct);
        return appointment.Id;
    }
}