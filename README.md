# CashFlow API

A simple expense management API developed during the Rocketseat C# trail.

## 💻 Technologies

- .NET 8
- ASP.NET Core
- Entity Framework Core
- MySQL
- FluentValidation
- Clean Architecture

## 🏗️ Architecture

The project follows Clean Architecture principles, organized in layers:

- **API**: HTTP requests handling and controllers
- **Application**: Use cases and business rules
- **Domain**: Entities and interfaces
- **Infrastructure**: Database access and external services
- **Communication**: DTOs for requests/responses
- **Exception**: Custom exception handling

## 🚀 Features

### Expenses Management
- Register new expenses
- Different payment types support (Cash, Credit Card, Debit Card, Electronic Transfer)
- Validation of expense data
- Culture-aware middleware for internationalization

## 📡 API Routes

### Expenses
- `POST /expenses` - Register a new expense
  ```json
  {
    "title": "Lunch",
    "description": "Restaurant",
    "date": "2024-01-20",
    "amount": 25.90,
    "paymentType": 0 // 0: Cash, 1: CreditCard, 2: DebitCard, 3: ElectronicTransfer
  }
  ```

## ⚙️ Running the Project

1. Clone the repository
```bash
git clone https://github.com/yourusername/CashFlow.git
```

2. Update the connection string in `appsettings.Development.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=cashflowdb;Uid=root;Pwd=yourpassword;"
  }
}
```

3. Run the project
```bash
dotnet run --project src/CashFlow.API/CashFlow.API.csproj
```

4. Access Swagger documentation
```
http://localhost:5139/swagger
```

## 📝 License

This project is licensed under the MIT License.

---

Made with 💜 during Rocketseat C# trail