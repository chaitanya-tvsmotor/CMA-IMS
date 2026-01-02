namespace CMA.Core.Entities;

public class VendorOrderItem : BaseEntity
{
    public int VendorOrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    
    // Navigation properties
    public VendorOrder VendorOrder { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
