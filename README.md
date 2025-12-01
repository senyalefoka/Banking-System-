# Banking System - ASP.NET Core Application

## 📋 Project Overview
A comprehensive banking system built with ASP.NET Core MVC, implementing clean architecture, repository pattern, and robust validation using FluentValidation.

## 🏗️ Architecture
```
Step_By_Step_Working_Project/
├── Project.Domain/                 # Domain Layer (Business Logic)
│   ├── Entities/                  # Domain Models
│   ├── Interfaces/                # Repository Contracts
│   └── Validators/                # FluentValidation Rules
├── Project.Infrastructure/        # Infrastructure Layer (Data Access)
│   └── Repositories/             # Repository Implementations
└── Project.UI/                    # Presentation Layer (MVC)
    ├── Controllers/              # MVC Controllers
    ├── Views/                    # Razor Views
    └── Program.cs               # Startup Configuration
```

## 🚀 Features
- ✅ **User Authentication** - Login and Registration system
- ✅ **Employee Management** - Full CRUD operations
- ✅ **Personal Details** - Employee information management
- ✅ **Account Management** - Banking account operations
- ✅ **Transaction Tracking** - Financial transaction history
- ✅ **Validation Layer** - FluentValidation integration
- ✅ **Clean Architecture** - Separation of concerns
- ✅ **Repository Pattern** - Abstracted data access

## 🛠️ Tech Stack
- **Backend**: ASP.NET Core 6.0+, C# 10
- **Frontend**: Razor Views, Bootstrap 5
- **Database**: Entity Framework Core, SQL Server
- **Validation**: FluentValidation 11.x
- **Architecture**: Clean Architecture, Repository Pattern

## 📦 Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## 🔧 Installation

### 1. Clone the Repository
```bash
git clone https://github.com/senyalefoka/Banking-System-.git
cd Banking-System-
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```

### 3. Configure Database
Update `appsettings.json` with your connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BankingSystemDB;Trusted_Connection=True;"
  }
}
```

### 4. Apply Database Migrations
```bash
dotnet ef database update
```

### 5. Run the Application
```bash
dotnet run
```
Navigate to: `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure Details

### Domain Layer (`Project.Domain`)
Contains business logic and validation rules:
- **Entities**: `PersonalDetail`, `Account`, `Transaction`
- **Interfaces**: Repository contracts
- **Validators**: FluentValidation rules for business logic

### Infrastructure Layer (`Project.Infrastructure`)
Data access implementation:
- **Repositories**: Entity Framework Core implementations
- **DbContext**: Database context configuration

### UI Layer (`Project.UI`)
ASP.NET Core MVC application:
- **Controllers**: `EmployeesController` with all CRUD operations
- **Views**: Razor views for all user interfaces
- **Program.cs**: Dependency injection setup

## 🎯 Key Implementation Features

### 1. **FluentValidation Integration**
```csharp
// Example: PersonalDetailsValidator.cs
public class PersonalDetailsValidator : AbstractValidator<PersonalDetail>
{
    public PersonalDetailsValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().MaximumLength(50);
            
        RuleFor(p => p.DateOfBirth)
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("Must be at least 18 years old");
    }
}
```

### 2. **Repository Pattern**
```csharp
public class PersonalDetailsRepository : IPersonalDetailsRepository
{
    // CRUD operations with validation
    public async Task<PersonalDetail> AddAsync(PersonalDetail personalDetail)
    {
        // Business logic and validation
        return await _context.SaveChangesAsync();
    }
}
```

### 3. **Dependency Injection Setup**
```csharp
// Program.cs
builder.Services.AddScoped<IPersonalDetailsRepository, PersonalDetailsRepository>();
builder.Services.AddScoped<IValidator<PersonalDetail>, PersonalDetailsValidator>();
```

## 🔌 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Employees` | Dashboard |
| GET | `/Employees/RegisterView` | Registration form |
| POST | `/Employees/RegisterView` | Register user |
| GET | `/Employees/LoginView` | Login form |
| POST | `/Employees/LoginView` | Authenticate |
| GET | `/Employees/Add` | Add employee |
| POST | `/Employees/Add` | Create employee |
| GET | `/Employees/Edit/{id}` | Edit employee |
| POST | `/Employees/Edit/{id}` | Update employee |
| GET | `/Employees/DetailsView` | View all employees |
| GET | `/Employees/AccountDetail` | View accounts |
| GET | `/Employees/TransactionDetail` | View transactions |

## 🧪 Testing

### Run Unit Tests
```bash
dotnet test
```

### Sample Test Structure
```csharp
[Fact]
public void PersonalDetailsValidator_ValidData_ShouldPass()
{
    var validator = new PersonalDetailsValidator();
    var personalDetail = new PersonalDetail 
    { 
        FirstName = "John",
        LastName = "Doe",
        DateOfBirth = DateTime.Now.AddYears(-25)
    };
    
    var result = validator.Validate(personalDetail);
    Assert.True(result.IsValid);
}
```

## 📊 Database Schema

### PersonalDetails Table
```sql
CREATE TABLE PersonalDetails (
    Code UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    IDNumber NVARCHAR(13) NOT NULL UNIQUE,
    AccountNumber NVARCHAR(20) NOT NULL UNIQUE,
    CreatedDate DATETIME2 DEFAULT GETDATE()
)
```

## 🚀 Deployment

### Publish to IIS
```bash
dotnet publish -c Release -o ./publish
```

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project.UI/Project.UI.csproj", "Project.UI/"]
RUN dotnet restore "Project.UI/Project.UI.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.UI.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License
Distributed under the MIT License. See `LICENSE` for more information.

## 📞 Support
- **Issues**: [GitHub Issues](https://github.com/senyalefoka/Banking-System-/issues)
- **Email**: your-email@example.com

## 🙏 Acknowledgments
- ASP.NET Core Team
- FluentValidation Library
- Entity Framework Core Team
- Bootstrap Team

---

**⭐ If you find this project useful, please give it a star on GitHub*

**Last Updated**: November 2024  
**Version**: 1.0.0  
**Author**: Matlou Sekgaphola  
**Status**: 🚀 Production Ready
