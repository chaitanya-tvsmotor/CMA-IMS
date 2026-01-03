namespace CMA.Core.Entities;

/// <summary>
/// Junction table linking Deliveries to Orders - one delivery can have multiple orders
/// </summary>
public class DeliveryOrder : BaseEntity
{
    public int DeliveryId { get; set; }
    public int OrderId { get; set; }
    public int SequenceNumber { get; set; }  // Order in which orders are delivered in the trip
    public DateTime? DeliveredAt { get; set; }
    public string? DeliveryNotes { get; set; }
    
    // Navigation properties
    public Delivery Delivery { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
