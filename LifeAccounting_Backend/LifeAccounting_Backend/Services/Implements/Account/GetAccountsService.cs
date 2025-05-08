using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class GetAccountsService : IGetAccountsService
    {
        private readonly LifeAccountingDbContext _context;

        public GetAccountsService(LifeAccountingDbContext context) 
        {
            _context = context;
        }

        // 取得使用者帳戶資料
        public async Task<object> GetAccountsAsync(int userId)
        {
            // 回傳帳戶列表
            var accountModels = await _context.Accounts
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AccountDTO
            {
                Id = a.Id,
                Name = a.Name,
                Currency = a.Currency,
                Balance = a.Balance,
                CreatedAt = a.CreatedAt
            }).ToListAsync();

            // 包裝回傳格式
            var result = new { items = accountModels };

            return result;
        }
    }
}
