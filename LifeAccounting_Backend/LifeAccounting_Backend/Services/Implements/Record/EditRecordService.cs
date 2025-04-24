using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class EditRecordService : IEditRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public EditRecordService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, bool Forbidden, string Message)> EditRecordAsync(int userId, int recordId, RecordEditDTO model)
        {
            // 找出該收支紀錄
            var record = await _context.Records.FindAsync(recordId);
            if (record == null)
            {
                return (false, false, "Record not found.");
            }

            // 驗證是否為該使用者的收支紀錄
            if (record.UserId != userId)
            {
                return (false, true, "You are not authorized to view this record.");
            }

            // 更新資料
            record.AccountId = model.AccountId;
            record.CategoryId = model.CategoryId;
            record.Amount = model.Amount;
            record.Note = model.Note;
            record.Date = model.Date;
            record.Type = model.Type;

            await _context.SaveChangesAsync();
            return (true, false, "Record updated successfully.");
        }
    }
}
