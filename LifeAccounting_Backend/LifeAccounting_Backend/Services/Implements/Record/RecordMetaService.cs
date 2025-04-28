using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class RecordMetaService : IRecordMetaService
    {
        private readonly LifeAccountingDbContext _context;

        public RecordMetaService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 取得帳戶和收支類型下拉選單選項
        public async Task<(List<OptionDTO> Accounts, List<OptionDTO> Categories)> GetMetaDataAsync(int userId)
        {
            var accounts = await _context.Accounts
                .Where(a => a.UserId == userId)
                .Select(a => new OptionDTO { Id = a.Id, Name = a.Name })
                .ToListAsync();

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .Select(c => new OptionDTO { Id = c.Id, Name = c.Name, Type = c.Type })
                .ToListAsync();

            return (accounts, categories);
        }
    }
}
