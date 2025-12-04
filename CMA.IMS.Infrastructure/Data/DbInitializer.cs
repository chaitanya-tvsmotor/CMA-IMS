using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CMA.IMS.Core.Entities;

namespace CMA.IMS.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create database if it doesn't exist
        await context.Database.EnsureCreatedAsync();

        // Seed roles
        string[] roles = { "Admin", "Manager", "Viewer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed admin user
        if (await userManager.FindByEmailAsync("admin@cma.com") == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin@cma.com",
                Email = "admin@cma.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
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

        if (!await context.Agents.AnyAsync())
        {
            var agents = new List<Agent>
            {
                new Agent { Name = "John Agent", Email = "john.agent@cma.com", Phone = "1231231234", EmployeeCode = "AG001" },
                new Agent { Name = "Sarah Agent", Email = "sarah.agent@cma.com", Phone = "3213213210", EmployeeCode = "AG002" }
            };
            await context.Agents.AddRangeAsync(agents);
            await context.SaveChangesAsync();
        }

        if (!await context.Customers.AnyAsync())
        {
            var agent = await context.Agents.FirstOrDefaultAsync();
            var customers = new List<Customer>
            {
                new Customer { Name = "ABC Retail Store", Email = "contact@abcretail.com", Phone = "1112223333", Company = "ABC Retail", AgentId = agent?.Id },
                new Customer { Name = "XYZ Supermarket", Email = "info@xyzsupermarket.com", Phone = "4445556666", Company = "XYZ Corp", AgentId = agent?.Id },
                new Customer { Name = "Metro Mart", Email = "sales@metromart.com", Phone = "7778889999", Company = "Metro Mart Ltd" }
            };
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }
}
