using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.Category;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Services.Implements.Category
{
    public class GetEditCategoryService : IGetEditCategoryService
    {
        private readonly LifeAccountingDbContext _context;

        public GetEditCategoryService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, bool Forbidden, string Message, CategoryEditDTO? Data)> GetEditCategoryAsync(int userId, int categoryId)
        {
            // 找出該收支類型
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return (false, false, "Category not found.", null);
            }

            // 驗證是否為該使用者的收支類型
            if (category.UserId != userId)
            {
                return (false, true, "You are not authorized to view this category.", null);
            }

            // 將該收支類型轉換成可編輯的資料模型
            var editCategoryModel = new CategoryEditDTO
            {
                Name = category.Name,
                Type = category.Type
            };

            return (true, false, "Success", editCategoryModel);
        }
    }
}
