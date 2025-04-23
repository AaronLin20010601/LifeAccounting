using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Category;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Services.Implements.Category
{
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly LifeAccountingDbContext _context;

        public CreateCategoryService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 建立新的收支類型
        public async Task<(bool Success, string Message)> CreateCategoryAsync(int userId, CategoryEditDTO model)
        {
            // 建立收支類型
            var category = new Models.Entities.Category
            {
                UserId = userId,
                Name = model.Name,
                Type = model.Type,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return (true, "Category created successfully!");
        }
    }
}
