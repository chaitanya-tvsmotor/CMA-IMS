namespace CMA.IMS.Core.Entities;

public class ProductSize : BaseEntity
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int StockQuantity { get; set; }
    
    // Navigation properties
    public Product Product { get; set; } = null!;
    public Size Size { get; set; } = null!;
}
