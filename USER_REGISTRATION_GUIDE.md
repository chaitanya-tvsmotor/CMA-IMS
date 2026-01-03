# CMA (Classic Marketing Agencies) - Inventory Management System

## Overview
This guide explains how to register and manage users with different roles in the CMA Inventory Management System.

## Pre-seeded Users

The system comes with pre-configured users for testing and immediate use:

| Role | Email | Password | Access Level |
|------|-------|----------|-------------|
| Admin | admin@cma.com | Admin@123 | Full system access |
| Manager | manager@cma.com | Manager@123 | Full operational access, can approve orders |
| Supervisor | supervisor@cma.com | Supervisor@123 | Same as Manager but Vendors and Purchase Orders are view-only |
| Sales Agent | salesagent@cma.com | SalesAgent@123 | Products, Orders, Dealers |
| Accountant | accountant@cma.com | Accountant@123 | View-only access to all data |
| Dealer | dealer@cma.com | Dealer@123 | View products with prices, create orders |

## User Roles and Permissions

### 1. Admin
**Access Level**: Full system control
- **Permissions**:
  - All Manager permissions
  - User management
  - System configuration
  - Full CRUD on all entities

### 2. Manager
**Access Level**: Full operational access
- **Permissions**:
  - Dashboard with statistics
  - Products (full CRUD)
  - Orders (full CRUD + approve/reject)
  - Employees (full CRUD)
  - Vehicles (full CRUD)
  - Purchase Orders (full CRUD)
  - Dealers (full CRUD)
  - Categories, Brands, Vendors (full CRUD)

### 3. Supervisor
**Access Level**: Same as Manager with limited write access on Vendors and Purchase Orders
- **Permissions**:
  - Dashboard with statistics
  - Products (full CRUD)
  - Orders (full CRUD)
  - Employees (full CRUD)
  - Vehicles (full CRUD)
  - Purchase Orders (**VIEW ONLY**)
  - Dealers (full CRUD)
  - Categories, Brands (full CRUD)
  - Vendors (**VIEW ONLY**)

### 4. Sales Agent
**Access Level**: Limited to sales operations
- **Permissions**:
  - Products (view and search)
  - Orders (create and manage)
  - Dealers (view and manage)

### 5. Accountant
**Access Level**: Read-only access
- **Permissions**:
  - Products (view only)
  - Orders (view only)
  - Employees (view only)
  - Vehicles (view only)
  - Purchase Orders (view only)
  - All data visible but no modifications allowed

### 6. Dealer
**Access Level**: Customer portal
- **Permissions**:
  - View products with prices
  - Create orders to sales agents
  - View own order history

### 7. Public (Anonymous)
**Access Level**: Public website
- **Permissions**:
  - Browse product catalog (without prices)
  - View About Us page
  - View Contact Us page

## How to Register New Users

### Option 1: Using Admin Account (Recommended)

1. **Login as Admin**
   - Navigate to: `http://localhost:5000/account/login`
   - Email: `admin@cma.com`
   - Password: `Admin@123`

2. **Access User Management** (Future Feature)
   - Currently, user registration must be done through code or database

### Option 2: Direct Database Registration

Since the UI for user registration is not yet implemented, you can add users programmatically:

#### Method 1: Update DbInitializer.cs

Add new users to the seed data in `CMA.IMS.Infrastructure/Data/DbInitializer.cs`:

```csharp
// Add to the usersToSeed dictionary:
{ "newuser", ("newuser@cma.com", "Password@123", "RoleName") }
```

Then delete the database and restart the application to re-seed.

#### Method 2: Using EF Core Migrations

Run the following command to create a migration for adding users:

```bash
dotnet ef migrations add AddNewUsers --project CMA.IMS.Infrastructure --startup-project CMA.IMS.Web
dotnet ef database update --project CMA.IMS.Infrastructure --startup-project CMA.IMS.Web
```

#### Method 3: Direct SQL (SQLite)

For quick testing, you can use SQL commands:

```bash
# Open SQLite database
cd CMA.IMS.Web
sqlite3 cma_ims.db

# Check existing users
SELECT Email FROM AspNetUsers;

# Exit
.exit
```

### Option 3: Programmatic Registration (Developer)

Create a simple endpoint or service to register users:

```csharp
public async Task<bool> RegisterUser(string email, string password, string role)
{
    var user = new IdentityUser
    {
        UserName = email,
        Email = email,
        EmailConfirmed = true
    };

    var result = await _userManager.CreateAsync(user, password);
    if (result.Succeeded)
    {
        await _userManager.AddToRoleAsync(user, role);
        return true;
    }
    return false;
}
```

## Password Requirements

All passwords must meet the following requirements:
- Minimum 8 characters
- At least one uppercase letter (A-Z)
- At least one lowercase letter (a-z)
- At least one digit (0-9)
- At least one special character (@, #, $, etc.)

**Examples of valid passwords:**
- `Admin@123`
- `Manager#2024`
- `SecurePass@1`

## Common Login Issues

### Issue 1: "Invalid login attempt"
**Solution**: 
- Check if email and password are correct
- Verify caps lock is off
- Ensure password meets requirements

### Issue 2: "User not found"
**Solution**:
- Verify user was created in database
- Check if user email is confirmed
- Restart application after adding new users

### Issue 3: "Access Denied"
**Solution**:
- Verify user has correct role assigned
- Check if role exists in system
- Ensure user is assigned to the correct role

## Testing Different Roles

To test different role permissions:

1. **Login as each pre-seeded user**
2. **Observe the navigation menu** - Each role sees different menu items
3. **Try accessing different pages** - View-only roles will show "View" buttons instead of "Edit/Delete"
4. **Test CRUD operations** - Some roles cannot create, update, or delete data

## Role Switching for Testing

To quickly switch between roles during development:

1. Logout from current session
2. Navigate to `/account/login`
3. Login with different user credentials
4. Observe different navigation and permissions

## Future Enhancements

### Planned Features:
1. **Self-Registration Portal** - Allow dealers to register themselves
2. **Admin User Management UI** - Create, edit, delete users from dashboard
3. **Password Reset** - Email-based password recovery
4. **User Profile Management** - Update personal information
5. **Two-Factor Authentication** - Enhanced security
6. **Role Management UI** - Dynamic role creation and assignment

## Security Best Practices

1. **Change default passwords** immediately in production
2. **Use strong passwords** for all accounts
3. **Limit Admin access** to trusted personnel only
4. **Regular password rotation** (every 90 days)
5. **Monitor user activity** through logs
6. **Disable inactive accounts** after 30 days

## Support and Contact

For issues related to user registration or access:
- **Email**: admin@cma.com
- **Phone**: +91 123-456-7890
- **Website**: http://localhost:5000

## Quick Reference

| Task | Command/Action |
|------|----------------|
| Login | Navigate to `/account/login` |
| Logout | Click user menu → Logout |
| Reset database | Delete `cma_ims.db` and restart app |
| View seeded users | Check `DbInitializer.cs` |
| Add new role | Update `DbInitializer.cs` roles array |

---

**Last Updated**: January 2026  
**Version**: 1.0  
**System**: CMA Inventory Management System
