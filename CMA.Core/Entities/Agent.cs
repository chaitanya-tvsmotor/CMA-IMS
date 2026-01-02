namespace CMA.Core.Entities;

// Note: This entity is being deprecated in favor of Employee with SalesAgent type
// Keeping for backward compatibility during migration
public class Agent : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? EmployeeCode { get; set; }
    public bool IsActive { get; set; } = true;
    public int? EmployeeId { get; set; }  // Link to Employee
    
    // Navigation properties
    public Employee? Employee { get; set; }
    public ICollection<Dealer> Dealers { get; set; } = new List<Dealer>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
