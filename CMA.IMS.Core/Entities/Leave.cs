namespace CMA.IMS.Core.Entities;

public class Leave : BaseEntity
{
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType LeaveType { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public string? Reason { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }
    
    // Navigation properties
    public Employee Employee { get; set; } = null!;
}

public enum LeaveType
{
    Sick,
    Casual,
    Earned,
    Unpaid
}

public enum LeaveStatus
{
    Pending,
    Approved,
    Rejected
}
