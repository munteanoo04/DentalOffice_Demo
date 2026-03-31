using DentalClinic.Domain.Enums;

namespace DentalClinic.Domain.Entities;

public class Procedure
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int DurationMinutes { get; private set; }
    public ProcedureCategory Category { get; private set; }
    public bool RequiresAnesthesia { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation — Appointment already connects Doctor + Patient + Procedure
    // NO Doctors collection here — wrong direction
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Procedure() { }

    public static Procedure Create(
        string name,
        string description,
        decimal price,
        int durationMinutes,
        ProcedureCategory category,
        bool requiresAnesthesia = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Procedure name is required.");
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");
        if (durationMinutes <= 0)
            throw new ArgumentException("Duration must be greater than zero.");

        return new Procedure
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            DurationMinutes = durationMinutes,
            Category = category,
            RequiresAnesthesia = requiresAnesthesia,
            IsActive = true
        };
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative.");
        Price = newPrice;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}

