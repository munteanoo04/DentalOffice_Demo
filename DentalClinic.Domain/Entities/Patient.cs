namespace DentalClinic.Domain.Entities;

public class Patient
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;

    // Optional calculated property
    public int Age => DateTime.Today.Year - DateOfBirth.Year;

    // Relationships
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}