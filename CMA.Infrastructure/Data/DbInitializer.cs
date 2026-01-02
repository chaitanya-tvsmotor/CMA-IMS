using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CMA.Core.Entities;

namespace CMA.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create database if it doesn't exist
        await context.Database.EnsureCreatedAsync();

        // Seed roles
        // Public - no role needed, anonymous access
        // Dealer - will have Dealer role
        // SalesAgent - Employee with SalesAgent type
        // Supervisor - Employee with Supervisor type
        // Manager - Employee with Manager type
        // Admin - full access
        string[] roles = { "Admin", "Manager", "Supervisor", "SalesAgent", "Accountant", "Dealer", "Public" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed users for all roles
        var usersToSeed = new Dictionary<string, (string email, string password, string role)>
        {
            { "admin", ("admin@cma.com", "Admin@123", "Admin") },
            { "manager", ("manager@cma.com", "Manager@123", "Manager") },
            { "supervisor", ("supervisor@cma.com", "Supervisor@123", "Supervisor") },
            { "salesagent", ("salesagent@cma.com", "SalesAgent@123", "SalesAgent") },
            { "accountant", ("accountant@cma.com", "Accountant@123", "Accountant") },
            { "dealer", ("dealer@cma.com", "Dealer@123", "Dealer") }
        };

        foreach (var userInfo in usersToSeed)
        {
            if (await userManager.FindByEmailAsync(userInfo.Value.email) == null)
            {
                var user = new IdentityUser
                {
                    UserName = userInfo.Value.email,
                    Email = userInfo.Value.email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, userInfo.Value.password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userInfo.Value.role);
                }
            }
        }

        // Seed sample data
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic items and gadgets" },
                new Category { Name = "Clothing", Description = "Apparel and fashion items" },
                new Category { Name = "Food & Beverages", Description = "Food items and drinks" },
                new Category { Name = "Home & Garden", Description = "Home and garden supplies" }
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Sizes.AnyAsync())
        {
            var sizes = new List<Size>
            {
                new Size { Name = "Small", Description = "S" },
                new Size { Name = "Medium", Description = "M" },
                new Size { Name = "Large", Description = "L" },
                new Size { Name = "Extra Large", Description = "XL" }
            };
            await context.Sizes.AddRangeAsync(sizes);
            await context.SaveChangesAsync();
        }

        if (!await context.Vendors.AnyAsync())
        {
            var vendors = new List<Vendor>
            {
                new Vendor { Name = "Tech Supplies Co.", ContactPerson = "John Smith", Email = "john@techsupplies.com", Phone = "1234567890" },
                new Vendor { Name = "Fashion Wholesale", ContactPerson = "Jane Doe", Email = "jane@fashionwholesale.com", Phone = "0987654321" },
                new Vendor { Name = "Food Distributors Inc.", ContactPerson = "Bob Johnson", Email = "bob@fooddist.com", Phone = "5551234567" }
            };
            await context.Vendors.AddRangeAsync(vendors);
            await context.SaveChangesAsync();
        }

        // Note: Agents are deprecated, using Employee.SalesAgent instead


        if (!await context.Employees.AnyAsync())
        {
            var employees = new List<Employee>
            {
                new Employee { Name = "John Manager", EmployeeCode = "EMP001", Email = "manager@cma.com", Phone = "1234567890", EmployeeType = EmployeeType.Manager, MonthlySalary = 80000, JoiningDate = DateTime.UtcNow.AddYears(-2) },
                new Employee { Name = "Jane Supervisor", EmployeeCode = "EMP002", Email = "supervisor@cma.com", Phone = "2345678901", EmployeeType = EmployeeType.Supervisor, MonthlySalary = 60000, JoiningDate = DateTime.UtcNow.AddYears(-1) },
                new Employee { Name = "Tom Sales Agent", EmployeeCode = "EMP003", Email = "salesagent@cma.com", Phone = "3456789012", EmployeeType = EmployeeType.SalesAgent, MonthlySalary = 45000, JoiningDate = DateTime.UtcNow.AddMonths(-10) },
                new Employee { Name = "Mike Worker", EmployeeCode = "EMP004", Email = "mike@cma.com", Phone = "4567890123", EmployeeType = EmployeeType.Worker, MonthlySalary = 30000, JoiningDate = DateTime.UtcNow.AddMonths(-6) },
                new Employee { Name = "Sarah Driver", EmployeeCode = "EMP005", Email = "sarah@cma.com", Phone = "5678901234", EmployeeType = EmployeeType.Driver, MonthlySalary = 35000, JoiningDate = DateTime.UtcNow.AddMonths(-8) },
                new Employee { Name = "Bob Accountant", EmployeeCode = "EMP006", Email = "accountant@cma.com", Phone = "6789012345", EmployeeType = EmployeeType.Accountant, MonthlySalary = 50000, JoiningDate = DateTime.UtcNow.AddMonths(-4) }
            };
            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }

        if (!await context.Dealers.AnyAsync())
        {
            var salesAgent = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeType == EmployeeType.SalesAgent);
            var dealers = new List<Dealer>
            {
                new Dealer { Name = "ABC Retail Store", Email = "dealer1@abcretail.com", Phone = "1112223333", Company = "ABC Retail", SalesAgentId = salesAgent?.Id },
                new Dealer { Name = "XYZ Supermarket", Email = "dealer2@xyzsupermarket.com", Phone = "4445556666", Company = "XYZ Corp", SalesAgentId = salesAgent?.Id },
                new Dealer { Name = "Metro Mart", Email = "dealer3@metromart.com", Phone = "7778889999", Company = "Metro Mart Ltd" }
            };
            await context.Dealers.AddRangeAsync(dealers);
            await context.SaveChangesAsync();
        }

        if (!await context.SubCategories.AnyAsync())
        {
            var electronics = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Electronics");
            var clothing = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Clothing");
            if (electronics != null || clothing != null)
            {
                var subCategories = new List<SubCategory>();
                if (electronics != null)
                {
                    subCategories.AddRange(new[]
                    {
                        new SubCategory { Name = "Mobile Phones", CategoryId = electronics.Id },
                        new SubCategory { Name = "Laptops", CategoryId = electronics.Id }
                    });
                }
                if (clothing != null)
                {
                    subCategories.AddRange(new[]
                    {
                        new SubCategory { Name = "Men's Wear", CategoryId = clothing.Id },
                        new SubCategory { Name = "Women's Wear", CategoryId = clothing.Id }
                    });
                }
                await context.SubCategories.AddRangeAsync(subCategories);
                await context.SaveChangesAsync();
            }
        }

        if (!await context.Brands.AnyAsync())
        {
            var brands = new List<Brand>
            {
                new Brand { Name = "Samsung", Description = "Electronics brand" },
                new Brand { Name = "Apple", Description = "Premium electronics" },
                new Brand { Name = "Nike", Description = "Sports brand" }
            };
            await context.Brands.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }

        if (!await context.Vehicles.AnyAsync())
        {
            var driver = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeType == EmployeeType.Driver);
            var vehicles = new List<Vehicle>
            {
                new Vehicle { VehicleNumber = "TN01AB1234", VehicleType = "Truck", Make = "Tata", Model = "407", Year = 2020, AssignedDriverId = driver?.Id },
                new Vehicle { VehicleNumber = "TN02CD5678", VehicleType = "Van", Make = "Mahindra", Model = "Bolero", Year = 2021 }
            };
            await context.Vehicles.AddRangeAsync(vehicles);
            await context.SaveChangesAsync();
        }
    }
}
