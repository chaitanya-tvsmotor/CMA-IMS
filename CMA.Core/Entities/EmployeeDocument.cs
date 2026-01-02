namespace CMA.Core.Entities;

public class EmployeeDocument : BaseEntity
{
    public int EmployeeId { get; set; }
    public string DocumentType { get; set; } = string.Empty;  // ID, Certificate, Contract, etc.
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? FilePath { get; set; }
    
    // Navigation properties
    public Employee Employee { get; set; } = null!;
}
