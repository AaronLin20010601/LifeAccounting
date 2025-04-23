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
        public async Task<(bool Success, bool Forbidden, string Message)> EditCategoryAsync(int userId, int categoryId, CategoryEditDTO model)
        {
            // 找出該收支類型
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return (false, false, "Category not found.");
            }

            // 驗證是否為該使用者的收支類型
            if (category.UserId != userId)
            {
                return (false, true, "You are not authorized to edit this category.");
            }

            // 更新資料
            category.Name = model.Name;
            category.Type = model.Type;

            await _context.SaveChangesAsync();
            return (true, false, "Category updated successfully.");
        }
    }
}
