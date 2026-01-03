namespace CMA.Core.Entities;

public class Attendance : BaseEntity
{
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;
    public TimeSpan? CheckInTime { get; set; }
    public TimeSpan? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? Remarks { get; set; }
    
    // Navigation properties
    public Employee Employee { get; set; } = null!;
}

public enum AttendanceStatus
{
    Present,
    Absent,
    HalfDay,
    Leave,
    Holiday
}
