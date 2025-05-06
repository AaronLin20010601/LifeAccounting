# Project Structure
-----
## Backend
```
LifeAccounting_Backend/
├── Controllers/
│   ├── BaseApiController.cs
│   ├── AccountController.cs
│   ├── CategoryController.cs
│   ├── ExcelController.cs
│   ├── LoginController.cs
│   ├── MetaController.cs
│   ├── RecordController.cs
│   ├── RegisterController.cs
│   ├── ResetController.cs
│   ├── UserController.cs
│   └── VerificationController.cs
│
├── Models/
│   ├── DTOs/
│   │   ├── Account/
│   │   │   ├── AccountDTO.cs
│   │   │   ├── AccountEditDTO.cs
│   │   │   └── TransferDTO.cs
│   │   │
│   │   ├── Category/
│   │   │   ├── CategoryDTO.cs
│   │   │   └── CategoryEditDTO.cs
│   │   │
│   │   ├── Currency/
│   │   │   ├── CurrencyRateDTO.cs
│   │   │   └── CurrencyResponseDTO.cs
│   │   │
│   │   ├── Login/
│   │   │   └── LoginDTO.cs
│   │   │
│   │   ├── Meta/
│   │   │   └── OptionDTO.cs
│   │   │
│   │   ├── Record/
│   │   │   ├── RecordDTO.cs
│   │   │   └── RecordEditDTO.cs
│   │   │
│   │   ├── Register/
│   │   │   └── RegisterDTO.cs
│   │   │
│   │   ├── Reset/
│   │   │   └── ResetDTO.cs
│   │   │
│   │   ├── User/
│   │   │   ├── SyncBalanceDTO.cs
│   │   │   └── UserDTO.cs
│   │   │
│   │   └── Verification/
│   │       └── EmailDTO.cs
│   │
│   ├── Entities/
│   │   ├── Account.cs
│   │   ├── Category.cs
│   │   ├── EmailLog.cs
│   │   ├── ExchangeRate.cs
│   │   ├── Record.cs
│   │   ├── ResetToken.cs
│   │   └── User.cs
│   │
│   └── LifeAccountingDbContext.cs
│
├── Services/
│   ├── Implements/
│   │   ├── Account/
│   │   │   ├── CreateAccountService.cs
│   │   │   ├── DeleteAccountService.cs
│   │   │   ├── EditAccountService.cs
│   │   │   ├── GetAccountsService.cs
│   │   │   ├── GetEditAccountService.cs
│   │   │   └── TransferBalanceService.cs
│   │   │
│   │   ├── Category/
│   │   │   ├── CreateCategoryService.cs
│   │   │   ├── DeleteCategoryService.cs
│   │   │   ├── EditCategoryService.cs
│   │   │   ├── GetCategoriesService.cs
│   │   │   └── GetEditCategoryService.cs
│   │   │
│   │   ├── Currency/
│   │   │   ├── CurrencyService.cs
│   │   │   └── ExchangeRateService.cs
│   │   │
│   │   ├── Email/
│   │   │   ├── EmailLogger.cs
│   │   │   ├── EmailSender.cs
│   │   │   └── EmailService.cs
│   │   │
│   │   ├── Excel/
│   │   │   └── RecordExportService.cs
│   │   │
│   │   ├── Login/
│   │   │   └── LoginService.cs
│   │   │
│   │   ├── Meta/
│   │   │   └── MetaService.cs
│   │   │
│   │   ├── Record/
│   │   │   ├── CreateRecordService.cs
│   │   │   ├── DeleteRecordService.cs
│   │   │   ├── EditRecordService.cs
│   │   │   ├── GetEditRecordService.cs
│   │   │   └── GetRecordsService.cs
│   │   │
│   │   ├── Register/
│   │   │   └── RegisterService.cs
│   │   │
│   │   ├── Reset/
│   │   │   └── ResetPasswordService.cs
│   │   │
│   │   ├── Token/
│   │   │   └── JwtTokenService.cs
│   │   │
│   │   ├── User/
│   │   │   ├── GetUserService.cs
│   │   │   └── SyncBalanceService.cs
│   │   │
│   │   └── VerifyCode/
│   │       ├── RegisterVerificationService.cs
│   │       ├── ResetVerificationService.cs
│   │       └── VerificationCode.cs
│   │
│   └── Interfaces/
│       ├── Account/
│       │   ├── ICreateAccountService.cs
│       │   ├── IDeleteAccountService.cs
│       │   ├── IEditAccountService.cs
│       │   ├── IGetAccountsService.cs
│       │   ├── IGetEditAccountService.cs
│       │   └── ITransferBalanceService.cs
│       │
│       ├── Category/
│       │   ├── ICreateCategoryService.cs
│       │   ├── IDeleteCategoryService.cs
│       │   ├── IEditCategoryService.cs
│       │   ├── IGetCategoriesService.cs
│       │   └── IGetEditCategoryService.cs
│       │
│       ├── Currency/
│       │   ├── ICurrencyService.cs
│       │   └── IExchangeRateService.cs
│       │
│       ├── Email/
│       │   ├── IEmailLogger.cs
│       │   ├── IEmailSender.cs
│       │   └── IEmailService.cs
│       │
│       ├── Excel/
│       │   └── IRecordExportService.cs
│       │
│       ├── Login/
│       │   └── ILoginService.cs
│       │
│       ├── Meta/
│       │   └── IMetaService.cs
│       │
│       ├── Record/
│       │   ├── ICreateRecordService.cs
│       │   ├── IDeleteRecordService.cs
│       │   ├── IEditRecordService.cs
│       │   ├── IGetEditRecordService.cs
│       │   └── IGetRecordsService.cs
│       │
│       ├── Register/
│       │   └── IRegisterService.cs
│       │
│       ├── Reset/
│       │   └── IResetPasswordService.cs
│       │
│       ├── Token/
│       │   └── IJwtTokenService.cs
│       │
│       ├── User/
│       │   ├── IGetUserService.cs
│       │   └── ISyncBalanceService.cs
│       │
│       └── VerifyCode/
│           ├── IVerificationCode.cs
│           └── IVerificationCodeService.cs
│
├── Settings/
│   └── MailjetSettings.cs
│
├── Program.cs
└── appsettings.json
```
## Frontend
```
LifeAccounting_Frontend/
├── src/
│   ├── api/
│   │   ├── account.js
│   │   ├── category.js
│   │   ├── excel.js
│   │   ├── login.js
│   │   ├── meta.js
│   │   ├── record.js
│   │   ├── register.js
│   │   ├── reset.js
│   │   ├── user.js
│   │   └── verification.js
│   │
│   ├── assets/
│   │   └── tailwind.css
│   │
│   ├── components/
│   │   ├── account/
│   │   │   ├── AccountTable.vue
│   │   │   ├── AddAccountForm.vue
│   │   │   ├── EditAccountForm.vue
│   │   │   └── TransferBalanceForm.vue
│   │   │
│   │   ├── category/
│   │   │   ├── AddCategoryForm.vue
│   │   │   ├── CategoryTable.vue
│   │   │   └── EditCategoryForm.vue
│   │   │
│   │   ├── chart/
│   │   │   ├── AccountDonutChart.vue
│   │   │   ├── CategoryHeatmap.vue
│   │   │   ├── DonutChart.vue
│   │   │   ├── IncomeExpensePieChart.vue
│   │   │   ├── MonthlyLineChart.vue
│   │   │   └── PieChart.vue
│   │   │
│   │   ├── record/
│   │   │   ├── AddRecordForm.vue
│   │   │   ├── EditRecordForm.vue
│   │   │   ├── Pagination.vue
│   │   │   ├── RecordFilterBar.vue
│   │   │   └── RecordTable.vue
│   │   │
│   │   ├── shared/
│   │   │   ├── Footer.vue
│   │   │   └── Navbar.vue
│   │   │
│   │   ├── LoginForm.vue
│   │   ├── RegisterForm.vue
│   │   ├── ResetForm.vue
│   │   └── VerificationForm.vue
│   │
│   ├── router/
│   │   └── index.js
│   │
│   ├── service/
│   │   ├── chartService.js
│   │   ├── errorService.js
│   │   └── excelService.js
│   │
│   ├── store/
│   │   └── index.js
│   │
│   ├── views/
│   │   ├── account/
│   │   │   ├── AccountView.vue
│   │   │   ├── AddAccountView.vue
│   │   │   ├── EditAccountView.vue
│   │   │   └── TransferBalanceView.vue
│   │   │
│   │   ├── category/
│   │   │   ├── AddCategoryView.vue
│   │   │   ├── CategoryView.vue
│   │   │   └── EditCategoryView.vue
│   │   │
│   │   ├── record/
│   │   │   ├── AddRecordView.vue
│   │   │   ├── EditRecordView.vue
│   │   │   └── RecordView.vue
│   │   │
│   │   ├── ChartView.vue
│   │   ├── LoginView.vue
│   │   ├── RegisterView.vue
│   │   └── ResetView.vue
│   │
│   ├── App.vue
│   └── main.js
│
├── package.json
└── tailwind.config.js
```
