using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.Entities;
using LifeAccounting_Backend.Services.Interfaces.Currency;

namespace LifeAccounting_Backend.Services.Implements.Currency
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly LifeAccountingDbContext _context;
        private readonly ICurrencyService _fetcher;

        public ExchangeRateService(LifeAccountingDbContext context, ICurrencyService fetcher)
        {
            _context = context;
            _fetcher = fetcher;
        }

        // 取得並更新當日匯率對
        public async Task SeedExchangeRatesAsync()
        {
            var existingRates = await _context.ExchangeRates.ToListAsync();

            // 如果沒有當日有效匯率才抓取 api
            if (!existingRates.Any() || existingRates.Min(r => r.UpdatedAt.Date) < DateTime.UtcNow.Date)
            {
                _context.ExchangeRates.RemoveRange(existingRates);
                await _context.SaveChangesAsync();

                var currencyPairs = new List<(string from, string to)>
                {
                    ("TWD", "USD"), ("USD", "TWD"),
                    ("TWD", "JPY"), ("JPY", "TWD"),
                    ("USD", "JPY"), ("JPY", "USD"),
                };

                foreach (var (from, to) in currencyPairs)
                {
                    var rate = await _fetcher.GetRateAsync(from, to);
                    _context.ExchangeRates.Add(new ExchangeRate
                    {
                        FromCurrency = from,
                        ToCurrency = to,
                        ToPrice = rate,
                        UpdatedAt = DateTime.UtcNow
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
