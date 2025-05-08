using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.Account;
using Microsoft.EntityFrameworkCore;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class DeleteAccountService :IDeleteAccountService
    {
        private readonly LifeAccountingDbContext _context;

        public DeleteAccountService (LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 刪除現有的帳戶
        public async Task<(bool Success, string Message)> DeleteAccountAsync(int userId, int accountId)
        {
            // 找出帳戶
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId);

            // 確認是否為該使用者的帳戶
            if (account == null)
            {
                return (false, "Account not found or you are not authorized to delete this account.");
            }

            try
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return (true, "Account deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete account: {ex.Message}");
            }
        }
    }
}
