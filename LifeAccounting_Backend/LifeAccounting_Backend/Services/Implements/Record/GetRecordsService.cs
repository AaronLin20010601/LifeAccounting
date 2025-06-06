﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<object> GetRecordsAsync(int userId, int? accountId, int? categoryId, string? type, DateTime ? startDate, DateTime? endDate, int page, int pageSize)
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

            // 取得資料並返回
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 分頁處理
            var records = await query
                .OrderByDescending(r => r.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RecordDTO
                {
                    Id = r.Id,
                    AccountId = r.AccountId,
                    AccountName = r.Account.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category != null ? r.Category.Name : null,
                    Amount = r.Amount,
                    Note = r.Note,
                    Date = r.Date,
                    Type = r.Type
                })
                .ToListAsync();

            // 包裝回傳格式
            return new
            {
                items = records,
                currentPage = page,
                pageSize,
                totalItems,
                totalPages
            };
        }
    }
}
