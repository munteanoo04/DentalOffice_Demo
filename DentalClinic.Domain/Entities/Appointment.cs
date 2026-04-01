using DentalClinic.Domain.Enums;

namespace DentalClinic.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ProcedureId { get; set; }

    public DateTime ScheduledAt { get; set; }
    public string TreatmentType { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    // Relationships
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Procedure Procedure { get; set; } = null!;
}