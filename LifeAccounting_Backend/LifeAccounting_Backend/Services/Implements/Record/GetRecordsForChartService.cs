using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Services.Implements.Record
{
    public class GetRecordsForChartService : IGetRecordsForChartService
    {
        private readonly LifeAccountingDbContext _context;

        public GetRecordsForChartService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 取得使用者收支紀錄
        public async Task<object> GetRecordsForChartAsync(int userId, int? accountId, int? categoryId, string? type, DateTime? startDate, DateTime? endDate, string? toCurrency = null)
        {
            var query = _context.Records.AsNoTracking().Where(r => r.UserId == userId);

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

            var records = await query
                .Select(r => new RecordDTO
                {
                    Id = r.Id,
                    AccountId = r.AccountId,
                    AccountName = r.Account.Name,
                    AccountCurrency = r.Account.Currency,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category != null ? r.Category.Name : null,
                    Amount = r.Amount,
                    Date = r.Date,
                    Type = r.Type
                })
                .ToListAsync();

            // 匯率轉換
            if (!string.IsNullOrEmpty(toCurrency))
            {
                var fromCurrencies = records.Select(r => r.AccountCurrency).Distinct().ToList();

                var exchangeRates = await _context.ExchangeRates
                    .Where(r => r.ToCurrency == toCurrency && fromCurrencies.Contains(r.FromCurrency))
                    .ToDictionaryAsync(r => r.FromCurrency, r => r.ToPrice);

                foreach (var record in records)
                {
                    if (record.AccountCurrency != toCurrency && exchangeRates.TryGetValue(record.AccountCurrency, out var rate))
                    {
                        record.Amount = Math.Round(record.Amount * rate, 2);
                    }
                }
            }

            // 包裝回傳格式
            return new { items = records };
        }
    }
}
