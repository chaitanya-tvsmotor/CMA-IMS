namespace CMA.Core.Entities;

public class SalaryPayment : BaseEntity
{
    public int EmployeeId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal Amount { get; set; }
    public decimal? Bonus { get; set; }
    public decimal? Deductions { get; set; }
    public decimal NetAmount { get; set; }
    public string? Remarks { get; set; }
    public string? PaidBy { get; set; }
    
    // Navigation properties
    public Employee Employee { get; set; } = null!;
}
