using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.Record;
using Microsoft.EntityFrameworkCore;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class DeleteRecordService : IDeleteRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public DeleteRecordService(LifeAccountingDbContext context) 
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> DeleteRecordAsync(int userId, int recordId)
        {
            // 找出收支紀錄
            var record = await _context.Records.FirstOrDefaultAsync(a => a.Id == recordId && a.UserId == userId);

            // 確認是否為該使用者的收支紀錄
            if (record == null)
            {
                return (false, "Record not found or you are not authorized to delete this record.");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // 同步更新帳戶餘額
            if (user.IsSync)
            {
                var account = await _context.Accounts.FindAsync(record.AccountId);
                if (account == null)
                {
                    return (false, "Account not found.");
                }

                if (record.Type == "Expense")
                {
                    account.Balance += record.Amount;
                }
                else if (record.Type == "Income" && account.Balance >= record.Amount)
                {
                    account.Balance -= record.Amount;
                }
                else
                {
                    return (false, "Insufficient balance.");
                }
            }

            try
            {
                _context.Records.Remove(record);
                await _context.SaveChangesAsync();
                return (true, "Record deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete record: {ex.Message}");
            }
        }
    }
}
