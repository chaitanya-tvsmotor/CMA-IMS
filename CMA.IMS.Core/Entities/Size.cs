namespace CMA.IMS.Core.Entities;

public class Size : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
