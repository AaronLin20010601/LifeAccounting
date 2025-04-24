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

        public async Task<(bool Success, bool Forbidden, string Message, RecordEditDTO? Data)> GetEditRecordAsync(int userId, int recordId)
        {
            // 找出該收支紀錄
            var record = await _context.Records.FindAsync(recordId);
            if (record == null)
            {
                return (false, false, "Record not found.", null);
            }

            // 驗證是否為該使用者的收支紀錄
            if (record.UserId != userId)
            {
                return (false, true, "You are not authorized to view this record.", null);
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

            return (true, false, "Success", editRecordModel);
        }
    }
}
