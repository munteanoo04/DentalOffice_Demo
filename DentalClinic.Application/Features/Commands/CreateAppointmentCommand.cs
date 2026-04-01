// Features/Commands/CreateAppointmentCommand.cs
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid ProcedureId,
    DateTime ScheduledAt,
    string TreatmentType,
    string? Notes
) : IRequest<Guid>;