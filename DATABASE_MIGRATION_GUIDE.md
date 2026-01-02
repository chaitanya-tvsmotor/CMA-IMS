# Database Migration Guide: SQLite to SQL Server

This guide explains how to migrate the Classic Marketing Agencies (CMA) Inventory Management System from SQLite to Microsoft SQL Server.

## Overview

The application now supports **both SQLite and SQL Server** database providers. You can switch between them using configuration settings without changing any code.

---

## Current Configuration

### Database Provider Selection

The database provider is controlled by the `DatabaseProvider` setting in `appsettings.json`:

```json
{
  "DatabaseProvider": "SqlServer"  // Options: "Sqlite" or "SqlServer"
}
```

- **Development**: Uses SQLite by default (set in `appsettings.Development.json`)
- **Production**: Uses SQL Server by default (set in `appsettings.json`)

### Connection Strings

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=cma_ims.db",
    "SqlServerConnection": "Server=localhost;Database=CMA_IMS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "DatabaseProvider": "SqlServer"
}
```

---

## Prerequisites for SQL Server

### 1. Install SQL Server

**Option A: SQL Server Express (Free)**
- Download from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Choose "Express" edition for development/small deployments

**Option B: SQL Server Developer Edition (Free)**
- Full-featured version for development purposes
- Download from same link above

**Option C: SQL Server on Docker**
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver --hostname sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Create Database

**Using SQL Server Management Studio (SSMS):**
1. Connect to your SQL Server instance
2. Right-click "Databases" → "New Database"
3. Name it "CMA_IMS"
4. Click OK

**Using Command Line:**
```sql
sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd"
CREATE DATABASE CMA_IMS;
GO
```

---

## Migration Steps

### Step 1: Update Connection String

Edit `appsettings.json` and update the `SqlServerConnection` with your SQL Server details:

```json
{
  "ConnectionStrings": {
    "SqlServerConnection": "Server=YOUR_SERVER;Database=CMA_IMS;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**Connection String Parameters:**
- `Server`: SQL Server instance name (e.g., `localhost`, `.\SQLEXPRESS`, `192.168.1.10`)
- `Database`: Database name (default: `CMA_IMS`)
- `User Id`: SQL Server username (e.g., `sa`)
- `Password`: User password
- `TrustServerCertificate=True`: Required for self-signed certificates
- `MultipleActiveResultSets=true`: Allows multiple active result sets

**Windows Authentication (Alternative):**
```json
"SqlServerConnection": "Server=localhost;Database=CMA_IMS;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

### Step 2: Set Database Provider

Edit `appsettings.json` and set:

```json
{
  "DatabaseProvider": "SqlServer"
}
```

### Step 3: Build and Run

```bash
# Restore packages (includes SQL Server provider)
dotnet restore

# Build the solution
dotnet build

# Run the application
cd CMA.Web
dotnet run
```

### Step 4: Verify Migration

The application will automatically:
1. ✅ Create all database tables and relationships
2. ✅ Apply Entity Framework Core migrations
3. ✅ Seed initial data (roles, users, sample data)
4. ✅ Initialize the database schema

**Pre-seeded users will be available:**
- admin@cma.com / Admin@123
- manager@cma.com / Manager@123
- supervisor@cma.com / Supervisor@123
- salesagent@cma.com / SalesAgent@123
- accountant@cma.com / Accountant@123
- dealer@cma.com / Dealer@123

---

## Verification & Testing

### 1. Check Database Creation

**Using SSMS:**
1. Connect to SQL Server
2. Expand "Databases"
3. You should see "CMA_IMS" database
4. Expand tables - you should see 24+ tables

**Using Command Line:**
```sql
sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -d CMA_IMS
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
GO
```

### 2. Verify Data Seeding

**Check for roles:**
```sql
SELECT * FROM AspNetRoles;
```

**Check for users:**
```sql
SELECT * FROM AspNetUsers;
```

**Check for sample data:**
```sql
SELECT * FROM Categories;
SELECT * FROM Products;
SELECT * FROM Dealers;
SELECT * FROM Employees;
```

### 3. Test CRUD Operations

Login to the application and test:

**Create Operations:**
1. ✅ Create a new Category
2. ✅ Create a new Product
3. ✅ Create a new Dealer
4. ✅ Create a new Order
5. ✅ Create a new Employee
6. ✅ Create a new Vehicle

**Read Operations:**
1. ✅ View all products
2. ✅ View all orders
3. ✅ View dashboard statistics

**Update Operations:**
1. ✅ Edit a product
2. ✅ Edit a dealer
3. ✅ Update order status

**Delete Operations:**
1. ✅ Delete a test category
2. ✅ Delete a test employee

---

## Entity Framework Core Migrations

### Create New Migration (If Needed)

```bash
cd CMA.Infrastructure
dotnet ef migrations add MigrationName --startup-project ../CMA.Web
```

### Apply Migrations Manually

```bash
cd CMA.Web
dotnet ef database update --project ../CMA.Infrastructure
```

### View Migration History

```sql
SELECT * FROM __EFMigrationsHistory;
```

---

## Switching Back to SQLite

To switch back to SQLite (for development/testing):

1. Edit `appsettings.Development.json`:
```json
{
  "DatabaseProvider": "Sqlite"
}
```

2. Run with Development environment:
```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

---

## Database Schema

The system includes **24 entities** across 6 functional areas:

### Product Management (8 entities)
- Categories
- SubCategories
- Brands
- Vendors
- Sizes
- Products
- ProductVendors (junction)
- ProductSizes (junction)

### Sales Management (5 entities)
- Dealers
- Agents
- Orders
- OrderItems
- DeliveryOrders (junction)

### Purchasing (2 entities)
- VendorOrders
- VendorOrderItems

### HR Management (5 entities)
- Employees
- EmployeeDocuments
- Attendance
- Leaves
- SalaryPayments

### Vehicle Management (4 entities)
- Vehicles
- VehicleDocuments
- VehicleMaintenance
- Deliveries

---

## Performance Considerations

### SQL Server Advantages:
1. **Better Performance**: Optimized for large datasets (10,000+ records)
2. **Advanced Features**: Stored procedures, triggers, indexed views
3. **Scalability**: Supports multiple concurrent users
4. **Backup/Recovery**: Built-in backup and restore capabilities
5. **Security**: Advanced security features (encryption, auditing)
6. **Enterprise Ready**: Production-grade reliability

### SQLite Advantages:
1. **Simplicity**: No server installation required
2. **Portability**: Single file database
3. **Development**: Quick setup for development/testing
4. **Lightweight**: Minimal resource usage

---

## Troubleshooting

### Error: "Login failed for user 'sa'"

**Solution**: Verify SQL Server authentication is enabled
```sql
-- Enable mixed mode authentication
-- In SSMS: Right-click server → Properties → Security → SQL Server and Windows Authentication mode
```

### Error: "Cannot open database CMA_IMS"

**Solution**: Create the database manually
```sql
CREATE DATABASE CMA_IMS;
```

### Error: "A network-related or instance-specific error"

**Solution 1**: Check SQL Server is running
```bash
# Windows
services.msc → SQL Server (MSSQLSERVER) → Start

# Docker
docker ps
docker start sqlserver
```

**Solution 2**: Enable TCP/IP protocol
1. Open SQL Server Configuration Manager
2. SQL Server Network Configuration → Protocols for MSSQLSERVER
3. Enable TCP/IP
4. Restart SQL Server service

### Error: "Foreign key constraint failed"

**Solution**: This should not occur after migration. If it does:
1. Check that all required fields are filled
2. Verify referential integrity in database
3. Review DbInitializer.cs for seed data issues

---

## Connection String Examples

### Local SQL Server Express
```
Server=.\SQLEXPRESS;Database=CMA_IMS;Integrated Security=True;TrustServerCertificate=True;
```

### Remote SQL Server
```
Server=192.168.1.100,1433;Database=CMA_IMS;User Id=sa;Password=Pass@123;TrustServerCertificate=True;
```

### Azure SQL Database
```
Server=tcp:yourserver.database.windows.net,1433;Database=CMA_IMS;User ID=yourusername;Password=yourpassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

### SQL Server with Named Instance
```
Server=DESKTOP-PC\SQLEXPRESS;Database=CMA_IMS;Integrated Security=True;TrustServerCertificate=True;
```

---

## Production Deployment Checklist

- [ ] Install SQL Server on production server
- [ ] Create CMA_IMS database
- [ ] Update connection string in appsettings.json
- [ ] Set DatabaseProvider to "SqlServer"
- [ ] Configure firewall rules (port 1433)
- [ ] Create SQL Server login for application
- [ ] Grant appropriate permissions to application user
- [ ] Configure SQL Server backup strategy
- [ ] Enable SSL/TLS for SQL Server connections
- [ ] Test all CRUD operations
- [ ] Verify data integrity
- [ ] Monitor performance and optimize indexes if needed

---

## Support

For issues or questions:
1. Review Entity Framework Core logs in console output
2. Check SQL Server error logs
3. Verify connection string accuracy
4. Ensure SQL Server service is running
5. Test connection using SQL Server Management Studio first

---

## Summary

The application is now configured to support both SQLite (development) and SQL Server (production). The migration is seamless - simply update the configuration and run the application. All database schema, relationships, and seed data are automatically created on first run.

**Current Status:**
- ✅ SQLite support (legacy/development)
- ✅ SQL Server support (production)
- ✅ Automatic schema creation
- ✅ Data seeding
- ✅ All CRUD operations functional
- ✅ Foreign key relationships maintained
- ✅ 24 entities fully configured
- ✅ Zero code changes required to switch providers
