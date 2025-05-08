using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Category;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Services.Implements.Category
{
    public class EditCategoryService : IEditCategoryService
    {
        private readonly LifeAccountingDbContext _context;

        public EditCategoryService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 編輯收支類型內容
        public async Task<(bool Success, string Message)> EditCategoryAsync(int userId, int categoryId, CategoryEditDTO model)
        {
            // 找出收支類型
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            // 確認是否為該使用者的收支類型
            if (category == null)
            {
                return (false, "Category not found or you are not authorized to delete this category.");
            }

            // 更新資料
            category.Name = model.Name;
            category.Type = model.Type;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Category updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update category: {ex.Message}");
            }
        }
    }
}
