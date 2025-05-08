using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Settings;
using LifeAccounting_Backend.Extensions;
using LifeAccounting_Backend.Services.Interfaces.Currency;

var builder = WebApplication.CreateBuilder(args);

// 加入 DbContext 並使用 PostgreSQL 連線
builder.Services.AddDbContext<LifeAccountingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 註冊 Settings
builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));

// 註冊 http client
builder.Services.AddHttpClient();

// 註冊 DI
builder.Services.RegisterApplications();

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

// 匯率初始化
using (var scope = app.Services.CreateScope())
{
    var exchangeRateService = scope.ServiceProvider.GetRequiredService<IExchangeRateService>();
    await exchangeRateService.SeedExchangeRatesAsync();
}

app.Run("http://localhost:5100");