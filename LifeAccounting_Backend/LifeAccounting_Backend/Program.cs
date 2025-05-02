using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Settings;

using LifeAccounting_Backend.Services.Interfaces.Email;
using LifeAccounting_Backend.Services.Interfaces.Token;
using LifeAccounting_Backend.Services.Interfaces.VerifyCode;
using LifeAccounting_Backend.Services.Interfaces.Login;
using LifeAccounting_Backend.Services.Interfaces.Register;
using LifeAccounting_Backend.Services.Interfaces.Reset;
using LifeAccounting_Backend.Services.Interfaces.Excel;
using LifeAccounting_Backend.Services.Interfaces.Account;
using LifeAccounting_Backend.Services.Interfaces.Category;
using LifeAccounting_Backend.Services.Interfaces.Record;

using LifeAccounting_Backend.Services.Implements.Email;
using LifeAccounting_Backend.Services.Implements.Token;
using LifeAccounting_Backend.Services.Implements.VerifyCode;
using LifeAccounting_Backend.Services.Implements.Login;
using LifeAccounting_Backend.Services.Implements.Register;
using LifeAccounting_Backend.Services.Implements.Reset;
using LifeAccounting_Backend.Services.Implements.Excel;
using LifeAccounting_Backend.Services.Implements.Account;
using LifeAccounting_Backend.Services.Implements.Category;
using LifeAccounting_Backend.Services.Implements.Record;
using LifeAccounting_Backend.Services.Interfaces.Meta;
using LifeAccounting_Backend.Services.Implements.Meta;
using LifeAccounting_Backend.Services.Interfaces.User;
using LifeAccounting_Backend.Services.Implements.User;


var builder = WebApplication.CreateBuilder(args);

// �[�J DbContext �èϥ� PostgreSQL �s�u
builder.Services.AddDbContext<LifeAccountingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���U Settings
builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));

// ���U DI
// Email Service
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailLogger, EmailLogger>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Verify Code Service
builder.Services.AddScoped<IVerificationCode, VerificationCode>();
builder.Services.AddScoped<IRegisterVerificationService, RegisterVerificationService>();
builder.Services.AddScoped<IResetVerificationService, ResetVerificationService>();

// Login Service
builder.Services.AddScoped<ILoginService, LoginService>();

// Register Service
builder.Services.AddScoped<IRegisterService, RegisterService>();

// Reset Service
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();

// Meta Service
builder.Services.AddScoped<IMetaService, MetaService>();

// Excel Service
builder.Services.AddScoped<IRecordExportService, RecordExportService>();

// User Service
builder.Services.AddScoped<IGetUserService, GetUserService>();
builder.Services.AddScoped<ISyncBalanceService, SyncBalanceService>();

// Account Service
builder.Services.AddScoped<IGetAccountsService, GetAccountsService>();
builder.Services.AddScoped<ITransferBalanceService, TransferBalanceService>();
builder.Services.AddScoped<ICreateAccountService, CreateAccountService>();
builder.Services.AddScoped<IGetEditAccountService, GetEditAccountService>();
builder.Services.AddScoped<IEditAccountService, EditAccountService>();
builder.Services.AddScoped<IDeleteAccountService, DeleteAccountService>();

// Category Service
builder.Services.AddScoped<IGetCategoriesService, GetCategoriesService>();
builder.Services.AddScoped<ICreateCategoryService, CreateCategoryService>();
builder.Services.AddScoped<IGetEditCategoryService, GetEditCategoryService>();
builder.Services.AddScoped<IEditCategoryService, EditCategoryService>();
builder.Services.AddScoped<IDeleteCategoryService, DeleteCategoryService>();

// Record Service
builder.Services.AddScoped<IGetRecordsService, GetRecordsService>();
builder.Services.AddScoped<ICreateRecordService, CreateRecordService>();
builder.Services.AddScoped<IGetEditRecordService, GetEditRecordService>();
builder.Services.AddScoped<IEditRecordService, EditRecordService>();
builder.Services.AddScoped<IDeleteRecordService, DeleteRecordService>();


// �[�J JWT ����
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

// �]�m CORS�A���\���ШD
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// ��L�A�ȵ��U
builder.Services.AddControllers();

var app = builder.Build();

// �]�m��줤����
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5100");