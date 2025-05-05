using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;
using Microsoft.EntityFrameworkCore;

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

            // 更新幣別
            if (account.Currency != model.Currency)
            {
                // 取得並檢查匯率
                var exchangeRate = await _context.ExchangeRates
                    .Where(r => r.FromCurrency == account.Currency && r.ToCurrency == model.Currency)
                    .FirstOrDefaultAsync();

                if (exchangeRate == null)
                {
                    return (false, false, $"Exchange rate from {account.Currency} to {model.Currency} not found.");
                }

                decimal rate = exchangeRate.ToPrice;

                // 轉換帳戶餘額
                if (account.Balance == model.Balance) 
                {
                    model.Balance = Math.Round(model.Balance * rate, 2);
                }

                // 轉換所有該帳戶的收支紀錄金額
                var records = await _context.Records.Where(r => r.AccountId == accountId).ToListAsync();

                foreach (var record in records)
                {
                    record.Amount = Math.Round(record.Amount * rate, 2);
                }
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
