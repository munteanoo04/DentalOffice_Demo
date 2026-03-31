

namespace DentalClinic.Domain.Entities;

public class Doctor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Specialization { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    // Navigation
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Doctor() { } // EF Core needs this

    public static Doctor Create(
        string name,
        string firstName,
        string email,
        string phoneNumber,
        string specialization)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(specialization))
            throw new ArgumentException("Specialization is required.");

        return new Doctor
        {
            Id = Guid.NewGuid(),
            Name = name,
            FirstName = firstName,
            Email = email,
            PhoneNumber = phoneNumber,
            Specialization = specialization,
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
