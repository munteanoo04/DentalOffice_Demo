
namespace DentalClinic.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public bool IsActive { get; private set; }

    // Calculated — no need to store Age in database
    public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;

    // Navigation
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Patient() { } // EF Core needs this

    public static Patient Create(
        string name,
        string firstName,
        string email,
        string phoneNumber,
        DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (dateOfBirth > DateTime.UtcNow)
            throw new ArgumentException("Date of birth cannot be in the future.");

        return new Patient
        {
            Id = Guid.NewGuid(),
            Name = name,
            FirstName = firstName,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            IsActive = true
        };
    }

    public void UpdateContact(string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}