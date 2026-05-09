# ASP.NET Core Bookstore Application

A full-featured ASP.NET Core application demonstrating the integration of **Razor Pages** and **MVC** architectures. This project was developed as part of a comprehensive engineering assignment, focusing on robust design patterns, custom middleware, and clean separation of concerns.

## Features

- **Hybrid Architecture:** Seamlessly integrates both MVC (for catalog and order processing) and Razor Pages (for book management, cart, and authentication).
- **Authentication & Authorization:** Custom role-based session management (`AuthFilter`, `AuthPageFilter`).
- **Book Management:** CRUD operations for books with custom data validation (e.g., `PriceRangeAttribute`).
- **Shopping Cart & Checkout:** Persistent session-based cart management and order processing workflow.
- **Data Access:** Utilizes the Generic Repository pattern coupled with Entity Framework Core (In-Memory Database).
- **Error Handling:** Global exception filters to gracefully handle errors and log exceptions.

## Tech Stack

- C# & ASP.NET Core 9.0 (or appropriate target framework)
- Entity Framework Core
- Razor Pages & ASP.NET Core MVC
- Bootstrap & CSS for responsive UI

## Getting Started

1. Clone the repository.
2. Open the project in your preferred IDE (Visual Studio / VS Code / Rider).
3. Run the application using `dotnet run` or through the IDE play button.
4. The database is pre-seeded. You can log in using:
   - **Admin:** `admin` / `password`
   - **User:** `user` / `password`

## Project Structure

- **`/Controllers`**: MVC Controllers for displaying book catalogs and handling order flows.
- **`/Pages`**: Razor Pages for focused features like Book Management, User Accounts, and the Shopping Cart.
- **`/Models`**: Core domain entities and custom validation attributes.
- **`/Repositories`**: Generic Repository interface and implementation for data abstraction.
- **`/Filters`**: Custom global exception filters and authorization constraints.
