namespace CMA.Core.Entities;

public class VehicleDocument : BaseEntity
{
    public int VehicleId { get; set; }
    public string DocumentType { get; set; } = string.Empty;  // Registration, Insurance, etc.
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? FilePath { get; set; }
    
    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
}
