using Microsoft.EntityFrameworkCore;
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
        public async Task<(bool Success, string Message)> EditAccountAsync(int userId, int accountId, AccountEditDTO model)
        {
            // 找出帳戶
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId);

            // 確認是否為該使用者的帳戶
            if (account == null)
            {
                return (false, "Account not found or you are not authorized to delete this account.");
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
                    return (false, $"Exchange rate from {account.Currency} to {model.Currency} not found.");
                }

                decimal rate = exchangeRate.ToPrice;

                // 轉換帳戶餘額
                if (account.Balance == model.Balance) 
                {
                    model.Balance = Math.Round(account.Balance * rate, 2);
                }

                // 轉換所有該帳戶的收支紀錄金額
                await _context.Records
                    .Where(r => r.AccountId == accountId)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(r => r.Amount, r => Math.Round(r.Amount * rate, 2)));
            }

            // 更新資料
            account.Name = model.Name;
            account.Currency = model.Currency;
            account.Balance = model.Balance;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Account updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update account: {ex.Message}");
            }
        }
    }
}
