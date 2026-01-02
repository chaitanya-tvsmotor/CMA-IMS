namespace CMA.Core.Entities;

public class Vendor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();
    public ICollection<VendorOrder> VendorOrders { get; set; } = new List<VendorOrder>();
}
