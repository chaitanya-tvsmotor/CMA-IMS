namespace CMA.IMS.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Foreign keys
    public int CategoryId { get; set; }
    
    // Navigation properties
    public Category Category { get; set; } = null!;
    public ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();
    public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
