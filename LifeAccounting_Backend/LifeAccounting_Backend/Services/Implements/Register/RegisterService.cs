using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.Entities;
using LifeAccounting_Backend.Models.DTOs.Register;
using LifeAccounting_Backend.Services.Interfaces.Register;

namespace LifeAccounting_Backend.Services.Implements.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly LifeAccountingDbContext _context;

        public RegisterService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 檢查並送出註冊資料
        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO model)
        {
            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return (false, "Invalid or expired verification code.");
            }

            // 註冊新用戶
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password), // 密碼加密
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 設置ResetToken為已使用
            resetToken.IsUsed = true;
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();

            return (true, "User registered successfully.");
        }
    }
}
