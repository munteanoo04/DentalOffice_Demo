namespace DentalClinic.Application.Contracts.DTOs;

public record AppointmentDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    Guid ProcedureId,
    string ProcedureName,
    DateTime ScheduledAt,
    string TreatmentType,
    string? Notes,
    string Status
);