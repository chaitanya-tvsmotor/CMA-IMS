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
    
    // Dimensions and Weight
    public decimal? Length { get; set; }  // in cm
    public decimal? Width { get; set; }   // in cm
    public decimal? Height { get; set; }  // in cm
    public decimal? Weight { get; set; }  // in kg
    
    // Foreign keys
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int? BrandId { get; set; }
    
    // Navigation properties
    public Category Category { get; set; } = null!;
    public SubCategory? SubCategory { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();
    public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
