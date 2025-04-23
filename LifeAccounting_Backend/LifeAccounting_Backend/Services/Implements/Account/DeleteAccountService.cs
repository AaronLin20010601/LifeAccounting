using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.Account;

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
        public async Task<(bool Success, bool Forbidden, string Message)> DeleteAccountAsync(int userId, int accountId)
        {
            // 找出帳戶
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null) 
            {
                return (false, false, "Account not found.");
            }

            // 確認是否為該使用者的帳戶
            if (account.UserId != userId) 
            {
                return (false, true, "You are not authorized to delete this account.");
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return (true, false, "Account deleted successfully.");
        }
    }
}
