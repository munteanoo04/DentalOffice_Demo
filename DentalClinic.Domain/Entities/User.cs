namespace DentalClinic.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Client";
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string passwordHash,
        string role = "Client")
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password is required.");

        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email.ToLower(),
            PhoneNumber = phoneNumber,
            PasswordHash = passwordHash,
            Role = role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Deactivate() => IsActive = false;
}