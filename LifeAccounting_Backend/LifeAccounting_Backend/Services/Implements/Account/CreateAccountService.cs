using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Services.Implements.Account
{
    public class CreateAccountService : ICreateAccountService
    {
        private readonly LifeAccountingDbContext _context;

        public CreateAccountService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 建立新的帳戶
        public async Task<(bool Success, string Message)> CreateAccountAsync(int userId, AccountEditDTO model)
        {
            // 建立帳戶
            var account = new Models.Entities.Account
            {
                UserId = userId,
                Name = model.Name,
                Currency = model.Currency,
                Balance = model.Balance,
                CreatedAt = DateTime.UtcNow
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return (true, "Account created successfully!");
        }
    }
}
