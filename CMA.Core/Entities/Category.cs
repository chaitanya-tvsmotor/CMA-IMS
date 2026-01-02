namespace CMA.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
