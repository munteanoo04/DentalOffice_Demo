using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record CreateAppointmentCommand(
    int PatientId,
    int DoctorId,
    int ProcedureId,
    DateTime ScheduledAt,
    string TreatmentType,
    string? Notes
) : IRequest<int>;