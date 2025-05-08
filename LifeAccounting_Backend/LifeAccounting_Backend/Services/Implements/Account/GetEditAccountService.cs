using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;
using Microsoft.EntityFrameworkCore;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class GetEditAccountService : IGetEditAccountService
    {
        private readonly LifeAccountingDbContext _context;

        public GetEditAccountService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, AccountEditDTO? Data)> GetEditAccountAsync(int userId, int accountId)
        {
            // 找出帳戶
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId);

            // 確認是否為該使用者的帳戶
            if (account == null)
            {
                return (false, "Account not found or you are not authorized to delete this account.", null);
            }

            // 將該帳戶轉換成可編輯的資料模型
            var editAccountModel = new AccountEditDTO
            {
                Name = account.Name,
                Currency = account.Currency,
                Balance = account.Balance,
            };

            return (true, "Success",  editAccountModel);
        }
    }
}
