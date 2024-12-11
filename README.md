# Banking System

## Project Description
This project is a banking system simulation developed using ASP.NET Core Web API. The application manages essential banking operations like customer accounts, cards, payments, and expenses.

## Features

### Customer Management:
- Register new customers.
- Manage customer details.
- Automatically assign a credit limit (10,000 TL).

### Account Management:
- Open accounts (an account is created automatically when a customer is registered).
- View and update account details.
- Activate or deactivate accounts.

### Card Management:
- Define account and credit cards.
- Change card status (active/inactive).
- Perform deposit, withdrawal, payment, and spending operations with account cards.
- Spend using credit cards within the credit limit.

### Transactions:
- **Account Card:** Deposit, withdraw, make payments, and spend if the balance is sufficient.
- **Credit Card:** Spend within the credit limit.
- Cards can be activated/deactivated; inactive cards cannot be used for transactions.
- Add a description (up to 100 characters) to each transaction.

## Technologies Used
- **ASP.NET Core Web API:** Development of RESTful services.
- **Entity Framework Core:** Database management and ORM (Object Relational Mapping).
- **PostgreSQL:** Database.
- **Fluent Validation:** Data validation.
- **AutoMapper:** Object-to-object mapping for data transformations.
- **Autofac:** IoC (Inversion of Control) container.
- **Swagger:** API documentation.
- **Visual Studio:** Development environment.

## Installation
Follow these steps to run the project:

### 1. Clone the Repository
```bash
git clone https://github.com/sedayeler/banking-system
cd banking-system
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Set Up the Database
- Edit the appsettings.json file to update the PostgreSQL connection string.
- Run migrations to create the database:
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

### 5. Explore the API with Swagger
After starting the application, open your browser and go to http://localhost:5000/swagger.
