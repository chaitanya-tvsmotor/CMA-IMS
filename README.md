# CMA Inventory Management System

A comprehensive Inventory Management System built with .NET 10 and Blazor Server following best practices.

## Features

### Product Management
- **Categories**: Organize products into different categories
- **Vendors**: Manage multiple vendors for products
- **Sizes**: Define different sizes for products
- **Products**: Track products with SKU, pricing, stock levels, and reorder points
- Support for multiple vendors and sizes per product

### Customer & Sales Management
- **Customers**: Manage customer information (renamed from Buyers)
- **Marketing Agents**: Agents who manage multiple customers
- **Orders**: Customer orders with status tracking
- **Agent Bulk Orders**: Create orders for multiple customers simultaneously

### Vendor & Purchase Management
- **Vendor Orders**: Purchase orders to vendors for inventory tracking
- Track order status, expected delivery dates, and amounts

### Employee Management
- **Employee Types**: Worker, Driver, Supervisor, Manager, Marketing Agent, Accountant
- **Salary Management**: Monthly salary payments with bonus and deductions
- **Leave Management**: Track employee leaves with approval workflow

### Authentication & Authorization
- ASP.NET Core Identity integration
- Role-based access control:
  - **Admin**: Full system access
  - **Manager**: Approve/reject orders, full operations access
  - **Supervisor**: Create purchase orders, manage products and employees
  - **Marketing Agent**: View stock, create/update customer orders
  - **Accountant**: View-only access to all data
  - **Viewer**: Basic view permissions

### Dashboard
- Real-time statistics (products, categories, vendors, orders)
- Low stock alerts
- Recent orders overview

## Technology Stack

- **.NET 10**: Latest .NET framework
- **Blazor Server**: Interactive server-side rendering
- **Entity Framework Core 10**: ORM for data access
- **SQLite**: Embedded database
- **ASP.NET Core Identity**: Authentication and authorization
- **Bootstrap 5.3**: Responsive UI framework

## Project Structure

```
CMA.IMS/
├── CMA.IMS.Core/              # Domain entities and interfaces
│   ├── Entities/              # Domain models
│   └── Interfaces/            # Repository interfaces
├── CMA.IMS.Infrastructure/    # Data access layer
│   ├── Data/                  # DbContext and initialization
│   └── Repositories/          # Repository implementations
└── CMA.IMS.Web/               # Blazor web application
    ├── Components/            # Razor components
    │   ├── Layout/            # Layout components
    │   └── Pages/             # Page components
    └── wwwroot/               # Static files
```

## Getting Started

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository
```bash
git clone https://github.com/chaitanya-tvsmotor/CMA-IMS.git
cd CMA-IMS
```

2. Build the solution
```bash
dotnet build
```

3. Run the application
```bash
cd CMA.IMS.Web
dotnet run
```

4. Open browser and navigate to `http://localhost:5000`

### Default Credentials
- **Email**: admin@cma.com
- **Password**: Admin@123

## Database

The application uses SQLite database (`cma_ims.db`) which is automatically created and seeded with sample data on first run.

### Sample Data Includes:
- Admin user with credentials
- Roles (Admin, Manager, Supervisor, MarketingAgent, Accountant, Viewer)
- Sample categories, sizes, vendors
- Sample agents and customers
- Sample employees

## Key Features Explained

### Agent Bulk Order Creation
Marketing agents can create orders for multiple customers in a single operation:
1. Select the agent
2. Choose multiple customers
3. Add products that will be ordered for all selected customers
4. Create all orders at once

### Role-Based Permissions
Different roles have different access levels:
- **Marketing Agent**: Can view inventory and create orders for their customers
- **Supervisor**: Can manage purchase orders, products, and employees
- **Manager**: Can approve/reject orders and access all operations
- **Accountant**: Has view-only access to all data for reporting

### Vendor Order Management
Track purchase orders to vendors separately from customer orders:
- Order tracking with expected delivery dates
- Status management (Pending, Confirmed, Shipped, Received, Cancelled)
- Links to vendor information

## Future Enhancements
- Report generation (sales, inventory, employee)
- Email notifications for low stock and order status
- Advanced search and filtering
- Barcode/QR code integration
- Mobile application
- Multi-warehouse support

## License
This project is licensed for use by CMA organization.

## Contributors
- Development Team at CMA