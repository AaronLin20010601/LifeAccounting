using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class TransferBalanceService : ITransferBalanceService
    {
        private readonly LifeAccountingDbContext _context;

        public TransferBalanceService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 帳戶餘額轉移
        public async Task<(bool Success, string Message)> TransferBalanceAsync(int userId, TransferDTO model)
        {
            var accounts = await _context.Accounts.Where(a => a.Id == model.FromAccountId || a.Id == model.ToAccountId).ToListAsync();

            var fromAccount = accounts.FirstOrDefault(a => a.Id == model.FromAccountId);
            var toAccount = accounts.FirstOrDefault(a => a.Id == model.ToAccountId);

            // 檢查帳號是否存在
            if (fromAccount == null || toAccount == null)
            {
                return (false, "Account not found.");
            }

            // 檢查帳戶是否屬於該使用者
            if (fromAccount.UserId != userId || toAccount.UserId != userId)
            {
                return (false, "You are not authorized to access these accounts.");
            }

            // 檢查帳號是否重複
            if (fromAccount.Id == toAccount.Id)
            {
                return (false, "Cannot transfer to the same account.");
            }

            // 檢查餘額是否足夠
            if (fromAccount.Balance < model.Amount)
            {
                return (false, "Insufficient balance.");
            }

            // 帳戶匯率轉換
            decimal transferAmount = model.Amount;
            if (fromAccount.Currency != toAccount.Currency)
            {
                var rate = await _context.ExchangeRates
                    .Where(r => r.FromCurrency == fromAccount.Currency && r.ToCurrency == toAccount.Currency)
                    .Select(r => r.ToPrice)
                    .FirstOrDefaultAsync();

                if (rate == 0)
                {
                    return (false, "Exchange rate not found for the specified currency conversion.");
                }
                transferAmount = Math.Round(model.Amount * rate, 2, MidpointRounding.AwayFromZero);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                fromAccount.Balance -= model.Amount;
                toAccount.Balance += transferAmount;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Balance transfer success.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Transfer failed: {ex.Message}");
            }
        }
    }
}
