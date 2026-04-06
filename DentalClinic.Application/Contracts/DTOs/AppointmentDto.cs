namespace DentalClinic.Application.Contracts.DTOs;

public record AppointmentDto(
    int Id,
    int PatientId,
    string PatientName,
    int DoctorId,
    string DoctorName,
    int ProcedureId,
    string ProcedureName,
    DateTime ScheduledAt,
    string TreatmentType,
    string? Notes,
    string Status
);