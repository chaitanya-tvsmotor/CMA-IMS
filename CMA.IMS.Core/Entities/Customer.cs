namespace CMA.IMS.Core.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Company { get; set; }
    public bool IsActive { get; set; } = true;
    public int? AgentId { get; set; }
    
    // Navigation properties
    public Agent? Agent { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
