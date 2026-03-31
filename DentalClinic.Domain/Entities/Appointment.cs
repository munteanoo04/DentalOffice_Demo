using DentalClinic.Domain.Enums;

namespace DentalClinic.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid ProcedureId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public string TreatmentType { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public AppointmentStatus Status { get; private set; }

    // Navigation properties
    public Patient Patient { get; private set; } = null!;
    public Doctor Doctor { get; private set; } = null!;
    public Procedure Procedure { get; private set; } = null!;

    private Appointment() { }

    public static Appointment Create(
        Guid patientId,
        Guid doctorId,
        Guid procedureId,
        DateTime scheduledAt,
        string treatmentType,
        string? notes = null)
    {
        if (scheduledAt < DateTime.UtcNow)
            throw new ArgumentException("Appointment cannot be scheduled in the past.");

        return new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            DoctorId = doctorId,
            ProcedureId = procedureId,
            ScheduledAt = scheduledAt,
            TreatmentType = treatmentType,
            Notes = notes,
            Status = AppointmentStatus.Scheduled
        };
    }

    // Business rules live here — NOT in Application handlers
    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed appointment.");

        Status = AppointmentStatus.Cancelled;
    }

    public void Complete(string? notes = null)
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled appointment.");

        Status = AppointmentStatus.Completed;
        if (notes != null) Notes = notes;
    }

    public void Reschedule(DateTime newDateTime)
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be rescheduled.");
        if (newDateTime < DateTime.UtcNow)
            throw new ArgumentException("New date cannot be in the past.");

        ScheduledAt = newDateTime;
    }
}