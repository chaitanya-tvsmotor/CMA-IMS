namespace CMA.Core.Entities;

public class Dealer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Company { get; set; }
    public bool IsActive { get; set; } = true;
    public int? SalesAgentId { get; set; }
    public string? UserId { get; set; }  // Link to Identity User for dealer login
    
    // Navigation properties
    public Employee? SalesAgent { get; set; }  // Sales agent assigned to this dealer
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
