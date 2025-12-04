namespace CMA.IMS.Core.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public decimal MonthlySalary { get; set; }
    public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
    public DateTime? LeavingDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? UserId { get; set; }  // Link to Identity User
    
    // Navigation properties
    public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    public ICollection<SalaryPayment> SalaryPayments { get; set; } = new List<SalaryPayment>();
}

public enum EmployeeType
{
    Worker,
    Driver,
    Supervisor,
    Manager,
    MarketingAgent,
    Accountant
}
