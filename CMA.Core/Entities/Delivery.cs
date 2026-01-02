namespace CMA.Core.Entities;

public class Delivery : BaseEntity
{
    public string DeliveryNumber { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;
    public int VehicleId { get; set; }
    public int? DriverId { get; set; }  // Employee who is a Driver
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Scheduled;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal? TotalDistance { get; set; }  // in km
    public string? Notes { get; set; }
    
    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public Employee? Driver { get; set; }
    public ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();
}

public enum DeliveryStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled
}
