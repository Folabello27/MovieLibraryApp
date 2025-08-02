# Movie Library Management System

A .NET 8 web application built with clean architecture, demonstrating:

- **Layered design**: Domain, Data, UI, API, Tests
- **Entity Framework Core** with migrations and seeding
- **Authentication & Authorization** using ASP.NET Identity
- **CRUD operations** via Razor Pages
- **Web API** for movie data
- **Unit testing** with xUnit
- **GitHub** with feature branches and clean history

## Getting Started

1. Clone the repo
2. Run `dotnet restore`
3. Update connection string if needed
4. Run `dotnet ef database update` from UI project
5. Run `dotnet run` in `MovieLibrary.UI`
6. Log in with:
   - Email: `admin@movielib.com`
   - Password: `Admin123!`