# Agent Agnostisk Platform

A .NET-based platform built using Clean Architecture principles for agent-agnostic operations.

## 🏗️ Architecture

The solution follows Clean Architecture with clear separation of concerns across multiple layers:

```
┌─────────────────────────────────────────────┐
│           AgentAgnostiskPlatform.API        │  ← Presentation Layer
├─────────────────────────────────────────────┤
│      Infrastructure    │   Persistence      │  ← External Layer
├─────────────────────────────────────────────┤
│         AgentAgnostiskPlatform.Application  │  ← Application Layer
├─────────────────────────────────────────────┤
│           AgentAgnostiskPlatform.Domain     │  ← Core Layer
└─────────────────────────────────────────────┘
```

### Projects

- **Domain** - Core business entities and domain logic
- **Application** - Application services, DTOs, interfaces, and validation
- **Infrastructure** - External integrations and third-party services
- **Persistence** - Data access layer with Entity Framework Core
- **API** - ASP.NET Core Web API endpoints
- **AppHost** - .NET Aspire orchestration
- **ServiceDefaults** - Shared Aspire service configuration

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or full version)
- An IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/larsk7cdk/agent_agnostisk_platform.git
   cd AgentAgnostiskPlatform
   ```

2. **Configure the database connection**

   Update the connection string in `Backend/src/AgentAgnostiskPlatform.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "AppDatabaseConnectionString": "Server=(localdb)\\mssqllocaldb;Database=AgentAgnostiskPlatform;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd Backend/src/AgentAgnostiskPlatform.Persistence
   dotnet ef database update --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj
   ```

4. **Build the solution**
   ```bash
   cd ../../..  # Back to root
   dotnet build
   ```

### Running the Application

#### Option 1: Run via .NET Aspire (Recommended)

```bash
dotnet run --project AgentAgnostiskPlatform.AppHost/AgentAgnostiskPlatform.AppHost.csproj
```

This will start the Aspire dashboard where you can monitor all services.

#### Option 2: Run API directly

```bash
dotnet run --project Backend/src/AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj
```

The API will be available at:
- HTTPS: `https://localhost:7XXX` (port will be shown in console)
- HTTP: `http://localhost:5XXX`
- Swagger UI: `https://localhost:7XXX/swagger`

## 🛠️ Technology Stack

- **Framework**: .NET 10.0
- **API**: ASP.NET Core Web API
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Documentation**: Swagger/OpenAPI
- **Orchestration**: .NET Aspire

## 📝 Development

### Adding a New Entity

1. Create entity in `Domain/Entities` (inherit from `BaseEntity`)
2. Add `DbSet<YourEntity>` to `AppDatabaseContext`
3. Create entity configuration in `Persistence/EntityConfigurations`
4. Add migration:
   ```bash
   cd Backend/src/AgentAgnostiskPlatform.Persistence
   dotnet ef migrations add AddYourEntity --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj
   dotnet ef database update --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj
   ```
5. Create DTOs and validators in `Application/Contracts/DTOs`
6. Create controller in `API/Controllers`

### Database Commands

```bash
# Add new migration
dotnet ef migrations add <MigrationName> --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj

# Update database
dotnet ef database update --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj

# Remove last migration
dotnet ef migrations remove --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj

# Drop database
dotnet ef database drop --startup-project ../AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj
```

### Running Tests

```bash
dotnet test
```

## 📚 API Documentation

When running in development mode, Swagger documentation is available at `/swagger`.

The API includes:
- Interactive API documentation
- Request/response examples
- Schema definitions
- Try-it-out functionality

## 🔧 Configuration

### Culture Settings
The application uses Danish culture (`da-DK`) for date and number formatting.

### CORS
Currently configured for `http://localhost:4200`. Update in `Program.cs` if needed.

### Logging
Logs are written to:
- Console (all environments)
- Files in `Backend/src/AgentAgnostiskPlatform.API/Logs/` (configurable via `appsettings.json`)

## 🏥 Health Checks

Health endpoint available at `/health` for monitoring and orchestration.

## 📄 License

[Specify your license here]

## 👥 Authors

Lars K. Christensen - AAU Master's Thesis Project

## 🤝 Contributing

[Add contribution guidelines if applicable]
