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

        // 刪除現有的帳戶
        public async Task<(bool Success, bool Forbidden, string Message)> DeleteCategoryAsync(int userId, int categoryId)
        {
            // 找出帳戶
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return (false, false, "Category not found.");
            }

            // 確認是否為該使用者的帳戶
            if (category.UserId != userId)
            {
                return (false, true, "You are not authorized to delete this category.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return (true, false, "Category deleted successfully.");
        }
    }
}
