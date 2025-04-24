using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class DeleteRecordService : IDeleteRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public DeleteRecordService(LifeAccountingDbContext context) 
        {
            _context = context;
        }

        public async Task<(bool Success, bool Forbidden, string Message)> DeleteRecordAsync(int userId, int recordId)
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

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return (true, false, "Record deleted successfully.");
        }
    }
}
