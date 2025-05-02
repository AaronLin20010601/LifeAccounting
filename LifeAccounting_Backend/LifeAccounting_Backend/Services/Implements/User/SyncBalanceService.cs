using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.User;

namespace LifeAccounting_Backend.Services.Implements.User
{
    public class SyncBalanceService : ISyncBalanceService
    {
        private readonly LifeAccountingDbContext _context;

        public SyncBalanceService(LifeAccountingDbContext context) 
        {
            _context = context;
        }

        // 更改帳戶餘額與收支紀錄同步狀態
        public async Task<bool> SyncBalanceAsync(int userId, bool isSync)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.IsSync = isSync;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
