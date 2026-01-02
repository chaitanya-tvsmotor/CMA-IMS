namespace CMA.Core.Entities;

public class Vehicle : BaseEntity
{
    public string VehicleNumber { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;  // Truck, Van, Car, etc.
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Color { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? InsuranceExpiryDate { get; set; }
    public int? AssignedDriverId { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public Employee? AssignedDriver { get; set; }
    public ICollection<VehicleDocument> Documents { get; set; } = new List<VehicleDocument>();
    public ICollection<VehicleMaintenance> MaintenanceRecords { get; set; } = new List<VehicleMaintenance>();
}
