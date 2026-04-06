namespace DentalClinic.Web.Models;

public class CreatePatientRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}

public class CreateDoctorRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}

public class CreateAppointmentRequest
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ProcedureId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string TreatmentType { get; set; } = string.Empty;
    public string? Notes { get; set; }
}