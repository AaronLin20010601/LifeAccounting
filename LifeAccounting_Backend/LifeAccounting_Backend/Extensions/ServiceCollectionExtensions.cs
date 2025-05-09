using LifeAccounting_Backend.Services.Interfaces.Email;
using LifeAccounting_Backend.Services.Interfaces.Token;
using LifeAccounting_Backend.Services.Interfaces.VerifyCode;
using LifeAccounting_Backend.Services.Interfaces.Currency;
using LifeAccounting_Backend.Services.Interfaces.Login;
using LifeAccounting_Backend.Services.Interfaces.Register;
using LifeAccounting_Backend.Services.Interfaces.Reset;
using LifeAccounting_Backend.Services.Interfaces.Meta;
using LifeAccounting_Backend.Services.Interfaces.Excel;
using LifeAccounting_Backend.Services.Interfaces.User;
using LifeAccounting_Backend.Services.Interfaces.Account;
using LifeAccounting_Backend.Services.Interfaces.Category;
using LifeAccounting_Backend.Services.Interfaces.Record;

using LifeAccounting_Backend.Services.Implements.Email;
using LifeAccounting_Backend.Services.Implements.Token;
using LifeAccounting_Backend.Services.Implements.VerifyCode;
using LifeAccounting_Backend.Services.Implements.Currency;
using LifeAccounting_Backend.Services.Implements.Login;
using LifeAccounting_Backend.Services.Implements.Register;
using LifeAccounting_Backend.Services.Implements.Reset;
using LifeAccounting_Backend.Services.Implements.Meta;
using LifeAccounting_Backend.Services.Implements.Excel;
using LifeAccounting_Backend.Services.Implements.User;
using LifeAccounting_Backend.Services.Implements.Account;
using LifeAccounting_Backend.Services.Implements.Category;
using LifeAccounting_Backend.Services.Implements.Record;

namespace LifeAccounting_Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplications(this IServiceCollection services)
        {
            // Email Service
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailLogger, EmailLogger>();
            services.AddScoped<IEmailService, EmailService>();

            // Token Service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Verify Code Service
            services.AddScoped<IVerificationCode, VerificationCode>();
            services.AddScoped<IRegisterVerificationService, RegisterVerificationService>();
            services.AddScoped<IResetVerificationService, ResetVerificationService>();

            // Exchange Rate Service
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            // Login Service
            services.AddScoped<ILoginService, LoginService>();

            // Register Service
            services.AddScoped<IRegisterService, RegisterService>();

            // Reset Service
            services.AddScoped<IResetPasswordService, ResetPasswordService>();

            // Meta Service
            services.AddScoped<IMetaService, MetaService>();

            // Excel Service
            services.AddScoped<IRecordExportService, RecordExportService>();

            // User Service
            services.AddScoped<IGetUserService, GetUserService>();
            services.AddScoped<ISyncBalanceService, SyncBalanceService>();

            // Account Service
            services.AddScoped<IGetAccountsService, GetAccountsService>();
            services.AddScoped<ITransferBalanceService, TransferBalanceService>();
            services.AddScoped<ICreateAccountService, CreateAccountService>();
            services.AddScoped<IGetEditAccountService, GetEditAccountService>();
            services.AddScoped<IEditAccountService, EditAccountService>();
            services.AddScoped<IDeleteAccountService, DeleteAccountService>();

            // Category Service
            services.AddScoped<IGetCategoriesService, GetCategoriesService>();
            services.AddScoped<ICreateCategoryService, CreateCategoryService>();
            services.AddScoped<IGetEditCategoryService, GetEditCategoryService>();
            services.AddScoped<IEditCategoryService, EditCategoryService>();
            services.AddScoped<IDeleteCategoryService, DeleteCategoryService>();

            // Record Service
            services.AddScoped<IGetRecordsService, GetRecordsService>();
            services.AddScoped<IGetRecordsForChartService, GetRecordsForChartService>();
            services.AddScoped<ICreateRecordService, CreateRecordService>();
            services.AddScoped<IGetEditRecordService, GetEditRecordService>();
            services.AddScoped<IEditRecordService, EditRecordService>();
            services.AddScoped<IDeleteRecordService, DeleteRecordService>();

            return services;
        }
    }
}
