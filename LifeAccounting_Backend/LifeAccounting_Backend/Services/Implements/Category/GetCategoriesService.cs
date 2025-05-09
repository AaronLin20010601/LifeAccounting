using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Category;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Services.Implements.Category
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly LifeAccountingDbContext _context;

        public GetCategoriesService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 取得使用者收支類型資料
        public async Task<object> GetCategoriesAsync(int userId)
        {
            // 回傳帳戶列表
            var categoryModels = await _context.Categories
                .Where(c => c.UserId == userId)
                .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
            }).ToListAsync();

            // 包裝回傳格式
            return new { items = categoryModels };
        }
    }
}
