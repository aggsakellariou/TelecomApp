# TelecomApp - Mobile Phone Billing and Customer Management System

TelecomApp is a web application built with ASP.NET Core Razor Pages that enables telecommunication companies to manage customers, plans, billing, and call records through a role-based system.

## Features

- **Multi-Role System:**
  - **Clients:** View/pay bills, browse call history
  - **Sellers:** Register clients, generate bills, change plans
  - **Administrators:** Create plans, manage system settings

- **Core Functionality:**
  - Complete client management (CRUD operations)
  - Phone plan creation and management
  - Billing generation and processing
  - Call history tracking
  - Secure login with role-based access

## Requirements

- .NET 8.0 SDK
- Visual Studio 2022
- Microsoft SQL Server 2022
- Entity Framework Core 9.0

## Installation

1. Clone this repository:

    ```sh
    git clone https://github.com/aggsakellariou/TelecomApp.git
    ```

2. Configure database connection in appsettings.json:

    ```sh
    "ConnectionStrings": { "DefaultConnection": "Server=your-server;Database=TelecomDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;" }
    ```

3. Apply migrations:

    ```sh
    Update-Database
    ```

4. Run the application** from Visual Studio or using `dotnet run`

## Tech Stack

- **Backend:** ASP.NET Core 8, Entity Framework Core 9
- **Frontend:** Razor Pages, Bootstrap
- **Database:** Microsoft SQL Server 2022
- **Patterns:** MVC architecture, Repository pattern
- **Authentication:** Custom role-based auth system
