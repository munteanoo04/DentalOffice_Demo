namespace DentalClinic.Web.Models;

public class AppointmentDto
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public string PatientName { get; set; } = "";

    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";   

    public int ProcedureId { get; set; }
    public string ProcedureName { get; set; } = ""; 

    public DateTime ScheduledAt { get; set; }

    public string TreatmentType { get; set; } = "";
    public string? Notes { get; set; }

    public string Status { get; set; } = "";
}



