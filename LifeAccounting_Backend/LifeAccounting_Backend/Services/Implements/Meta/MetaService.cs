using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Meta;
using LifeAccounting_Backend.Services.Interfaces.Meta;
using LifeAccounting_Backend.Models.Entities;

namespace LifeAccounting_Backend.Services.Implements.Meta
{
    public class MetaService : IMetaService
    {
        private readonly LifeAccountingDbContext _context;

        public MetaService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 取得帳戶和收支類型下拉選單選項
        public async Task<(List<OptionDTO> Accounts, List<OptionDTO> Categories)> GetMetaDataAsync(int userId, string? toCurrency)
        {
            var rates = await _context.ExchangeRates
                .Where(r => r.ToCurrency == toCurrency)
                .ToListAsync();

            var accounts = await _context.Accounts
                .Where(a => a.UserId == userId)
                .Select(a => new { a.Id, a.Name, a.Balance, a.Currency }).ToListAsync();

            var convertedAccounts = accounts.Select(a => {
                var rate = rates.FirstOrDefault(r => r.FromCurrency == a.Currency);
                var convertedBalance = (rate != null && rate.ToPrice != 0) ? a.Balance * rate.ToPrice : a.Balance;
                return new OptionDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Balance = Math.Round(convertedBalance, 2),
                };
            }).ToList();

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .Select(c => new OptionDTO { Id = c.Id, Name = c.Name, Type = c.Type })
                .ToListAsync();

            return (convertedAccounts, categories);
        }
    }
}
