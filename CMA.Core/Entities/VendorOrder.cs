namespace CMA.Core.Entities;

public class VendorOrder : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int VendorId { get; set; }
    public VendorOrderStatus Status { get; set; } = VendorOrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public Vendor Vendor { get; set; } = null!;
    public ICollection<VendorOrderItem> VendorOrderItems { get; set; } = new List<VendorOrderItem>();
}

public enum VendorOrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Received,
    Cancelled
}
