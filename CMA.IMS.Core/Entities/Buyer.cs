namespace CMA.IMS.Core.Entities;

public class Buyer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Company { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
