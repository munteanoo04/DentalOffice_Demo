namespace DentalClinic.Domain.Entities;

public class Doctor
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Relationships
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}