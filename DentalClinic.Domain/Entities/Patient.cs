namespace DentalClinic.Domain.Entities;

public class Patient
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public bool IsActive { get; private set; }

    public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Patient() { }

    public static Patient Create(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        return new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            IsActive = true
        };
    }

    public void UpdateContact(string email, string phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}