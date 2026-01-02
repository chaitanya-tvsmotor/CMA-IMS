namespace CMA.IMS.Core.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int DealerId { get; set; }
    public int? SalesAgentId { get; set; }  // Employee who is a Sales Agent
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public string? ApprovedBy { get; set; }  // Manager who approved
    public DateTime? ApprovedDate { get; set; }
    
    // Navigation properties
    public Dealer Dealer { get; set; } = null!;
    public Employee? SalesAgent { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Rejected
}
