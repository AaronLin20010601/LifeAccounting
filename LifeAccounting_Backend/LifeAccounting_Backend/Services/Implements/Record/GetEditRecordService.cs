using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class GetEditRecordService : IGetEditRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public GetEditRecordService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, RecordEditDTO? Data)> GetEditRecordAsync(int userId, int recordId)
        {
            // 找出收支紀錄
            var record = await _context.Records.FirstOrDefaultAsync(a => a.Id == recordId && a.UserId == userId);

            // 確認是否為該使用者的收支紀錄
            if (record == null)
            {
                return (false, "Record not found or you are not authorized to delete this record.", null);
            }

            // 將該帳戶轉換成可編輯的資料模型
            var editRecordModel = new RecordEditDTO
            {
                AccountId = record.AccountId,
                CategoryId = record.CategoryId,
                Amount = record.Amount,
                Date = record.Date,
                Note = record.Note,
                Type = record.Type
            };

            return (true, "Success", editRecordModel);
        }
    }
}
