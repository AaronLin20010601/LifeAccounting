using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Models.Entities;
using LifeAccounting_Backend.Services.Interfaces.Record;
using Microsoft.EntityFrameworkCore;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class EditRecordService : IEditRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public EditRecordService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> EditRecordAsync(int userId, int recordId, RecordEditDTO model)
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
                var originalAccount = await _context.Accounts.FindAsync(record.AccountId);
                var newAccount = record.AccountId == model.AccountId ? originalAccount : await _context.Accounts.FindAsync(model.AccountId);

                if (originalAccount == null || newAccount == null)
                {
                    return (false, "Account not found.");
                }

                // 回復原帳戶餘額
                if (record.Type == "Expense")
                {
                    originalAccount.Balance += record.Amount;
                }
                else if (record.Type == "Income" && originalAccount.Balance >= record.Amount)
                {
                    originalAccount.Balance -= record.Amount;
                }
                else
                {
                    return (false, "Insufficient balance.");
                }

                // 調整新帳戶餘額
                if (model.Type == "Income")
                {
                    newAccount.Balance += model.Amount;
                }
                else if (model.Type == "Expense" && newAccount.Balance >= model.Amount)
                {
                    newAccount.Balance -= model.Amount;
                }
                else
                {
                    return (false, "Insufficient balance.");
                }
            }

            // 更新資料
            record.AccountId = model.AccountId;
            record.CategoryId = model.CategoryId;
            record.Amount = model.Amount;
            record.Note = model.Note;
            record.Date = model.Date;
            record.Type = model.Type;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Record updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update record: {ex.Message}");
            }
        }
    }
}
