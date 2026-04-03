namespace DentalClinic.Web.Models;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int ProcedureId { get; set; }
    public string ProcedureName { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public string TreatmentType { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
}