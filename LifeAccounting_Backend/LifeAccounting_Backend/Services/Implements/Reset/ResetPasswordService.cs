using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Reset;
using LifeAccounting_Backend.Services.Interfaces.Reset;

namespace LifeAccounting_Backend.Services.Implements.Reset
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly LifeAccountingDbContext _context;

        public ResetPasswordService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 檢查並送出重設密碼資料
        public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetDTO model)
        {
            // 查找ResetToken，確認Email和驗證碼
            var resetToken = await _context.ResetTokens
                .FirstOrDefaultAsync(rt => rt.Email == model.Email && rt.Token == model.VerificationCode && !rt.IsUsed);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.UtcNow)
            {
                return (false, "Invalid or expired verification code.");
            }

            // 檢查是否有該用戶
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // 更新密碼
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Users.Update(user);

            // 設置ResetToken為已使用
            resetToken.IsUsed = true;
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();

            return (true, "Password has been reset successfully.");
        }
    }
}
