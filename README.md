# LifeAccounting
A practice project of using .net core api and vue.js  

-----
### Backend:
.NET core api  
### Frontend:
vue.js  
### Database:
PostgreSQL on cloud database Supabase  

-----
## Additional packages added
### .NET Backend:
- BCrypt:For password hashing
- Mailjet:For mail sending
- JwtBearer:For JWT token verification
- Npgsql:For connection with PorstgreSQL
- ClosedXML:For excel export
  
### Vue Frontend:
- Tailwindcss:For css styling
- Vuex:For data storage and sync
- Axios:For api request
- Apexcharts:For financial charts display
- Plotly.js:For financial heatmap chart display

-----
## Data model design
- User:User's basic data and infos.
  - Columns:Id, Username, Email, PasswordHash, CreatedAt, IsSync.  
  
- Account:User's own accounts, like bank, wallet, etc.
  - Columns:Id, UserId, Name, Currency, CreatedAt, Balance.
  
- Category:Income and expense category that define by user.
  - Columns:Id, UserId, Name, Type.
  
- Record:Life accounting records of user.
  - Columns:Id, UserId, AccountId, CategoryId, Amount, Note, Date, Type, CreatedAt.
  
- EmailLog:Email logs record.
  - Columns:Id, ToEmail, Subject, Body, SentAt, IsSuccess.
  
- ResetToken:Tokens using on register and reset password.
  - Columns:Id, Token, Email, CreatedAt, ExpirationDate, IsUsed.
  
- ExchangeRate:Currencies exchange rates.
  - Columns:Id, FromCurrency, ToCurrency, ToPrize, UpdatedAt.

-----
## Technical Architecture & Design
This project follows a clean architecture with clear separation of concerns between layers:
  
- Backend (ASP.NET Core API) is built with a modular structure using:
  - DTOs for input/output separation from database entities.
  - Entity Framework Core (Code First) for database access with migration support.
  - Service Layer with Interfaces to encapsulate business logic and promote testability and scalability.
  - Data Annotations & Fluent Validation for backend-side data validation.
  - JWT Token Authentication with middleware to protect routes and manage user sessions.
  - Stateless API Design following RESTful principles, ensuring consistent and scalable API behavior.
  
- Frontend (Vue.js) is structured with component-based design and:
  - Vue Router to manage client-side routing.
  - Vuex for central state management and token persistence across views.
  - Axios for asynchronous API communication with token-based headers.
  - Form Validation with Custom Error Handling for synchronized front-end and back-end validation feedback.
  - Component Modularization for improved reusability and maintainability.
  - Chart Visualization using ApexCharts and Plotly.js to display various financial statistics.
  
The backend and frontend are fully decoupled, enabling independent development, testing, and deployment.  
The application is developed using Supabase PostgreSQL as a managed cloud database service.

-----
## Project setup and compile on development
For backend part:  
1. Create a visual studio 2022 .net core web api project in LifeAccounting folder.
2. When need to compile and test via cmd, enter the following commands:  
```sh
cd LifeAccounting/LifeAccounting_Backend/LifeAccounting_Backend
```  
```sh
dotnet run
```  
  
For frontend part:  
1. Create a new vue default project by the following commands and steps:  
```sh
cd LifeAccounting
```  
```sh
npm create vite@latest LifeAccounting_Frontend
```  
Select framework as vue, choose variant as Offical Vue Starter.  
Select Router and ESLint.  
```sh
npm install
```  

3. When need to compile and test via cmd, enter the following commands:  
```sh
cd LifeAccounting/LifeAccounting_Frontend
```  
```sh
npm run dev
```  

-----
## Features
1. User registration and login:
  - Users can register by providing an email, username, password and verification code. A verification code email will be sent via Mailjet.
  - Login with email and password, with JWT token generation for secure access.
  
2. Password reset:
  - Users can reset their password by providing email, then users can enter new password and verification code they get. A verification code email will be sent via Mailjet.
  
3. Income & expense record management:
  - Add, edit, view, and delete individual income or expense records.
  - Filter records by date, account, category, or type.
  - Synchronize account balance when record is created, edited, or deleted.
  
4. Account management:
  - Create multiple personal accounts (e.g., bank, cash).
  - View, edit, or delete accounts.
  - Support for different currencies and balance transfer.
  
5. Category management:
  - Define custom income and expense categories.
  
6. Excel export:
  - Download financial records as Excel files for offline analysis or backup.
  
7. Account balance sync:
  - If sync is enabled, account balances are automatically updated in real-time with financial record operations.
  
8. Financial statistics charts:
  - Monthly income and expense trends line chart.
  - Income and expense pie charts by category.
  - Donut charts showing income & expense distribution by account.
  - Category heatmap showing usage frequency across months.

9. Currency exchange rate support:
  - Fetches exchange rate data from currencyapi.com on application startup if rates are missing or outdated.
  - Supports multiple currencies (e.g., TWD, USD, JPY) for account management and real-time conversion in statistics and summaries.
