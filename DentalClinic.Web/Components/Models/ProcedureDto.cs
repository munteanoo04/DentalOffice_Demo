namespace DentalClinic.Web.Models;

public class ProcedureDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool RequiresAnesthesia { get; set; }
    public bool IsActive { get; set; }
}