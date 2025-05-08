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
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Type))
            {
                return (false, "Invalid category data.");
            }

            // 建立收支類型
            var category = new Models.Entities.Category
            {
                UserId = userId,
                Name = model.Name,
                Type = model.Type,
            };

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return (true, "Category created successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to create category: {ex.Message}");
            }
        }
    }
}
