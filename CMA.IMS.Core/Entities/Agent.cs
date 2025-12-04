namespace CMA.IMS.Core.Entities;

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
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
