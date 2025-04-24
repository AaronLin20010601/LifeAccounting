using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class CreateRecordService : ICreateRecordService
    {
        private readonly LifeAccountingDbContext _context;

        public CreateRecordService (LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 建立新的收支紀錄
        public async Task<(bool Success, string Message)> CreateRecordAsync(int userId, RecordEditDTO model)
        {
            // 建立收支紀錄
            var record = new Models.Entities.Record
            {
                UserId = userId,
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                Amount = model.Amount,
                Note = model.Note,
                Date = model.Date,
                Type = model.Type,
                CreatedAt = DateTime.UtcNow
            };

            _context.Records.Add(record);
            await _context.SaveChangesAsync();
            return (true, "Record created successfully!");
        }
    }
}
