using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class EditAccountService : IEditAccountService
    {
        private readonly LifeAccountingDbContext _context;

        public EditAccountService(LifeAccountingDbContext context) 
        {
            _context = context;
        }

        // 編輯帳戶內容
        public async Task<(bool Success, bool Forbidden, string Message)> EditAccountAsync(int userId, int accountId, AccountEditDTO model)
        {
            // 找出該帳戶
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return (false, false, "Account not found.");
            }

            // 驗證是否為該使用者的帳戶
            if (account.UserId != userId) 
            {
                return (false, true, "You are not authorized to edit this account.");
            }

            // 更新資料
            account.Name = model.Name;
            account.Currency = model.Currency;
            account.Balance = model.Balance;

            await _context.SaveChangesAsync();
            return (true, false, "Account updated successfully.");
        }
    }
}
