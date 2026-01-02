using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMA.IMS.Core.Entities;

namespace CMA.IMS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVendor> ProductVendors { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<VendorOrder> VendorOrders { get; set; }
    public DbSet<VendorOrderItem> VendorOrderItems { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<SalaryPayment> SalaryPayments { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleDocument> VehicleDocuments { get; set; }
    public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SKU).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.Length).HasPrecision(10, 2);
            entity.Property(e => e.Width).HasPrecision(10, 2);
            entity.Property(e => e.Height).HasPrecision(10, 2);
            entity.Property(e => e.Weight).HasPrecision(10, 2);
            
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(p => p.SubCategory)
                .WithMany(sc => sc.Products)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Configure Vendor
        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        // Configure Size
        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        // Configure ProductVendor
        modelBuilder.Entity<ProductVendor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VendorPrice).HasPrecision(18, 2);
            
            entity.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVendors)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(pv => pv.Vendor)
                .WithMany(v => v.ProductVendors)
                .HasForeignKey(pv => pv.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ProductSize
        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Dealer
        modelBuilder.Entity<Dealer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            
            entity.HasOne(d => d.SalesAgent)
                .WithMany()
                .HasForeignKey(d => d.SalesAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Agent (deprecated - using Employee.SalesAgent instead)
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
        });

        // Configure Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            
            entity.HasOne(o => o.Dealer)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DealerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(o => o.SalesAgent)
                .WithMany()
                .HasForeignKey(o => o.SalesAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure OrderItem
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure VendorOrder
        modelBuilder.Entity<VendorOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            
            entity.HasOne(vo => vo.Vendor)
                .WithMany(v => v.VendorOrders)
                .HasForeignKey(vo => vo.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure VendorOrderItem
        modelBuilder.Entity<VendorOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            
            entity.HasOne(voi => voi.VendorOrder)
                .WithMany(vo => vo.VendorOrderItems)
                .HasForeignKey(voi => voi.VendorOrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(voi => voi.Product)
                .WithMany()
                .HasForeignKey(voi => voi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.EmployeeCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.MonthlySalary).HasPrecision(18, 2);
        });

        // Configure Agent-Employee relationship
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Leave
        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(l => l.Employee)
                .WithMany(e => e.Leaves)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure SalaryPayment
        modelBuilder.Entity<SalaryPayment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Bonus).HasPrecision(18, 2);
            entity.Property(e => e.Deductions).HasPrecision(18, 2);
            entity.Property(e => e.NetAmount).HasPrecision(18, 2);
            
            entity.HasOne(sp => sp.Employee)
                .WithMany(e => e.SalaryPayments)
                .HasForeignKey(sp => sp.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure SubCategory
        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            
            entity.HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Brand
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Configure Vehicle
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VehicleNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.VehicleType).IsRequired().HasMaxLength(50);
            
            entity.HasOne(v => v.AssignedDriver)
                .WithMany()
                .HasForeignKey(v => v.AssignedDriverId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure VehicleDocument
        modelBuilder.Entity<VehicleDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DocumentType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(100);
            
            entity.HasOne(vd => vd.Vehicle)
                .WithMany(v => v.Documents)
                .HasForeignKey(vd => vd.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure VehicleMaintenance
        modelBuilder.Entity<VehicleMaintenance>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MaintenanceType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
            
            entity.HasOne(vm => vm.Vehicle)
                .WithMany(v => v.MaintenanceRecords)
                .HasForeignKey(vm => vm.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure EmployeeDocument
        modelBuilder.Entity<EmployeeDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DocumentType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(100);
            
            entity.HasOne(ed => ed.Employee)
                .WithMany(e => e.Documents)
                .HasForeignKey(ed => ed.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Attendance
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(a => a.Employee)
                .WithMany(e => e.AttendanceRecords)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
