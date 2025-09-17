# Online Shopping Project

## Overview

This is a C# ASP.NET Core Online Shopping System designed with a modular, microservice-based architecture. The project demonstrates best practices in clean architecture, RESTful API design, and modern .NET development. It currently includes a ProductService microservice with Entity Framework Core and SQLite for persistence.

## Features

- Modular microservice structure (ProductService, OrderService, etc.)
- ASP.NET Core Web API (minimal API style)
- Entity Framework Core with SQLite database
- Follows C# conventions for folder and file organization
- Swagger UI for API exploration and testing

## Folder Structure

```
OnlineShoppingProject/
│
├── .github/
├── Docs/
├── Deploy/
├── Gateway/
│   └── ApiGateway/
├── Services/
│   ├── ProductService/
│   │   ├── Data/
│   │   │   └── ProductDbContext.cs
│   │   ├── Models/
│   │   │   └── Product.cs
│   │   ├── Services/
│   │   │   └── ProductService.cs
│   │   └── Program.cs
│   ├── OrderService/
│   ├── PaymentService/
│   └── InventoryService/
├── Shared/
└── Online Shopping System Project Guide.docx
```

## Getting Started

### Prerequisites

- [.NET 7+ SDK](https://dotnet.microsoft.com/download)
- (Optional) Visual Studio or VS Code

### Build and Run

1. **Clone the repository:**
   ```
   git clone https://github.com/your-username/your-repo-name.git
   cd OnlineShoppingProject/Services/ProductService
   ```

2. **Restore dependencies:**
   ```
   dotnet restore
   ```

3. **Build the project:**
   ```
   dotnet build
   ```

4. **Apply database migrations:**
   ```
   dotnet ef database update
   ```

5. **Run the service:**
   ```
   dotnet run
   ```

6. **Explore the API:**
   - Open [http://localhost:5109/swagger](http://localhost:5109/swagger) in your browser.

## Conventions

- **Models:** `Models/` folder, PascalCase naming.
- **Services:** `Services/` folder, dependency injection via constructor.
- **Data:** `Data/` folder for EF Core DbContext and migrations.
- **Configuration:** Connection strings in `appsettings.json`.
- **Documentation:** High-level docs in `.docx`, technical/code docs in Markdown.

## Project Guide

See `Online Shopping System Project Guide.docx` for requirements, goals, and architectural decisions.

## Contribution

1. Fork the repo and create your branch.
2. Commit your changes following C# and project conventions.
3. Open a pull request for review.

---