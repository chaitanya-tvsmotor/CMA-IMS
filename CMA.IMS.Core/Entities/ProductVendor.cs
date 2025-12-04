namespace CMA.IMS.Core.Entities;

public class ProductVendor : BaseEntity
{
    public int ProductId { get; set; }
    public int VendorId { get; set; }
    public decimal VendorPrice { get; set; }
    public bool IsPreferred { get; set; }
    
    // Navigation properties
    public Product Product { get; set; } = null!;
    public Vendor Vendor { get; set; } = null!;
}
