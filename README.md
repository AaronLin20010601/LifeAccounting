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
  
### Vue Frontend:
- Tailwindcss:For css styling
- Vuex:For data storage and sync
- Axios:For api request

-----
## Data model design
- User:User's basic data and infos.
  - Columns:Id, Username, Email, PasswordHash, CreatedAt.  
  
- Account:User's own accounts, like bank, wallet, etc.
  - Columns:Id, UserId, Name, Currency, CreatedAt.
  
- Category:Income and expense category that define by user.
  - Columns:Id, UserId, Name, Type.
  
- Record:Life accounting records of user.
  - Columns:Id, UserId, AccountId, CategoryId, Amount, Note, Date, Type, CreatedAt.
  
- EmailLog:Email logs record.
  - Columns:Id, ToEmail, Subject, Body, SentAt, IsSuccess.
  
- ResetToken:Tokens using on register and reset password.
  - Columns:Id, Token, Email, CreatedAt, ExpirationDate, IsUsed.

-----
## Technical Architecture & Design
-----
## Project setup and compile on development
For backend part:  
1. Create a visual studio 2022 .net core web api project in Todolist folder.
2. When need to compile and test via cmd, enter the following commands:  
```sh
cd Todolist/Todolist_Backend/Todolist_Backend
```  
```sh
dotnet run
```  
  
For frontend part:  
1. Create a new vue default project by the following commands and steps:  
```sh
cd Todolist
```  
```sh
npm create vite@latest Todolist_Frontend
```  
Select framework as vue, choose variant as Offical Vue Starter.  
Select Router and ESLint.  
```sh
npm install
```  

3. When need to compile and test via cmd, enter the following commands:  
```sh
cd Todolist/Todolist_Frontend
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
