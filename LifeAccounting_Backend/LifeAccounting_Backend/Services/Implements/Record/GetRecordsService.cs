using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class GetRecordsService : IGetRecordsService
    {
        private readonly LifeAccountingDbContext _context;

        public GetRecordsService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 取得使用者收支紀錄
        public async Task<object> GetRecordsAsync(int userId, int? accountId, int? categoryId, string? type, DateTime ? startDate, DateTime? endDate, int page, int pageSize, string? toCurrency = null)
        {
            var query = _context.Records.Include(r => r.Account).Include(r => r.Category).Where(r => r.UserId == userId);

            // 篩選帳戶
            if (accountId.HasValue)
                query = query.Where(r => r.AccountId == accountId);

            // 篩選收支類型
            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId);

            // 篩選類型
            if (!string.IsNullOrEmpty(type))
                query = query.Where(r => r.Type == type);

            // 篩選時間區間
            if (startDate.HasValue)
                query = query.Where(r => r.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(r => r.Date <= endDate.Value);

            // 取得資料並返回
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 分頁處理
            var records = await query
                .OrderByDescending(r => r.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new
                {
                    r.Id,
                    r.AccountId,
                    AccountName = r.Account.Name,
                    AccountCurrency = r.Account.Currency,
                    r.CategoryId,
                    CategoryName = r.Category != null ? r.Category.Name : null,
                    r.Amount,
                    r.Note,
                    r.Date,
                    r.Type
                })
                .ToListAsync();

            // 匯率表
            var exchangeRates = new Dictionary<string, decimal>();
            if (!string.IsNullOrEmpty(toCurrency))
            {
                exchangeRates = await _context.ExchangeRates
                    .Where(r => r.ToCurrency == toCurrency)
                    .ToDictionaryAsync(r => r.FromCurrency, r => r.ToPrice);
            }

            // 回傳收支紀錄列表
            var recordModels = records.Select(r =>
            {
                // 檢查是否依匯率調整
                decimal convertedAmount = r.Amount;

                if (!string.IsNullOrEmpty(toCurrency) && r.AccountCurrency != toCurrency && exchangeRates.TryGetValue(r.AccountCurrency, out var rate))
                {
                    convertedAmount = r.Amount * rate;
                }

                return new RecordDTO
                {
                    Id = r.Id,
                    AccountId = r.AccountId,
                    AccountName = r.AccountName,
                    CategoryId = r.CategoryId,
                    CategoryName = r.CategoryName,
                    Amount = Math.Round(convertedAmount, 2),
                    Note = r.Note,
                    Date = r.Date,
                    Type = r.Type
                };
            }).ToList();

            // 包裝回傳格式
            var result = new
            {
                items = recordModels,
                currentPage = page,
                pageSize,
                totalItems,
                totalPages
            };

            return result;
        }
    }
}
