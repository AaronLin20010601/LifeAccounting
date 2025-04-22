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

using LifeAccounting_Backend.Services.Implements.Email;
using LifeAccounting_Backend.Services.Implements.Token;
using LifeAccounting_Backend.Services.Implements.VerifyCode;
using LifeAccounting_Backend.Services.Implements.Login;
using LifeAccounting_Backend.Services.Implements.Register;
using LifeAccounting_Backend.Services.Implements.Reset;

var builder = WebApplication.CreateBuilder(args);

// 加入 DbContext 並使用 PostgreSQL 連線
builder.Services.AddDbContext<LifeAccountingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 註冊 Settings
builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));

// 註冊 DI
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

// 加入 JWT 驗證
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

// 設置 CORS，允許跨域請求
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// 其他服務註冊
builder.Services.AddControllers();

var app = builder.Build();

// 設置跨域中間件
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5100");