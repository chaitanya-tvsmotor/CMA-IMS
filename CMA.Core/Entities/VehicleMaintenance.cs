namespace CMA.Core.Entities;

public class VehicleMaintenance : BaseEntity
{
    public int VehicleId { get; set; }
    public DateTime MaintenanceDate { get; set; } = DateTime.UtcNow;
    public string MaintenanceType { get; set; } = string.Empty;  // Service, Repair, Inspection
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public int? Odometer { get; set; }
    public string? ServiceProvider { get; set; }
    public string? InvoiceNumber { get; set; }
    
    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
}
