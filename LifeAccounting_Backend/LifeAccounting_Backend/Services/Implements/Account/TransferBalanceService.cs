using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;
using Microsoft.EntityFrameworkCore;

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
            var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.FromAccountId);
            var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.ToAccountId);

            // 檢查帳號是否存在
            if (fromAccount == null || toAccount == null)
            {
                return (false, "Account not found.");
            }

            // 檢查帳號是否重複
            if (fromAccount.Id == toAccount.Id)
            {
                return (false, "Connot transfer to the same account.");
            }

            // 檢查餘額是否足夠
            if (fromAccount.Balance < model.Amount)
            {
                return (false, "Insufficient balance.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            fromAccount.Balance -= model.Amount;
            toAccount.Balance += model.Amount;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, "Balance transfer success.");
        }
    }
}
