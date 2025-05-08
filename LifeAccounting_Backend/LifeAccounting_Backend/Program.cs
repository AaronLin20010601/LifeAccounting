using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Settings;
using LifeAccounting_Backend.Extensions;
using LifeAccounting_Backend.Services.Interfaces.Currency;

var builder = WebApplication.CreateBuilder(args);

// �[�J DbContext �èϥ� PostgreSQL �s�u
builder.Services.AddDbContext<LifeAccountingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���U Settings
builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));

// ���U http client
builder.Services.AddHttpClient();

// ���U DI
builder.Services.RegisterApplications();

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

// �ײv��l��
using (var scope = app.Services.CreateScope())
{
    var exchangeRateService = scope.ServiceProvider.GetRequiredService<IExchangeRateService>();
    await exchangeRateService.SeedExchangeRatesAsync();
}

app.Run("http://localhost:5100");