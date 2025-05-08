using Microsoft.EntityFrameworkCore;
using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Services.Implements.Category
{
    public class DeleteCategoryService : IDeleteCategoryService
    {
        private readonly LifeAccountingDbContext _context;

        public DeleteCategoryService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 刪除現有的收支類型
        public async Task<(bool Success, string Message)> DeleteCategoryAsync(int userId, int categoryId)
        {
            // 找出收支類型
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            // 確認是否為該使用者的收支類型
            if (category == null)
            {
                return (false, "Category not found or you are not authorized to delete this category.");
            }

            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return (true, "Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete category: {ex.Message}");
            }
        }
    }
}
