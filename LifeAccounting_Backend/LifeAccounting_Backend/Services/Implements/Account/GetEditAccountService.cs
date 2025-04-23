using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class GetEditAccountService : IGetEditAccountService
    {
        private readonly LifeAccountingDbContext _context;

        public GetEditAccountService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, bool Forbidden, string Message, AccountEditDTO? Data)> GetEditAccountAsync(int userId, int accountId)
        {
            // 找出該帳戶
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return (false,  false, "Account not found.", null);
            }

            // 驗證是否為該使用者的帳戶
            if (account.UserId != userId) 
            {
                return (false, true, "You are not authorized to view this account.", null);
            }

            // 將該帳戶轉換成可編輯的資料模型
            var editAccountModel = new AccountEditDTO
            {
                Name = account.Name,
                Currency = account.Currency
            };

            return (true, false, "Success",  editAccountModel);
        }
    }
}
