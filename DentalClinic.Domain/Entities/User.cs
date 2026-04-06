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
}