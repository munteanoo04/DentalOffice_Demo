namespace DentalClinic.Web.Models;

public class PatientDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";

    public int Age { get; set; }   // 🔥 FIX pentru @p.Age

    public bool IsActive { get; set; }
}

public class CreatePatientRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }
}