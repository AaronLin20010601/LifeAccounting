using Microsoft.EntityFrameworkCore;
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

        public async Task<(bool Success, string Message, CategoryEditDTO? Data)> GetEditCategoryAsync(int userId, int categoryId)
        {
            // 找出收支類型
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            // 確認是否為該使用者的收支類型
            if (category == null)
            {
                return (false, "Category not found or you are not authorized to delete this category.", null);
            }

            // 將該收支類型轉換成可編輯的資料模型
            var editCategoryModel = new CategoryEditDTO
            {
                Name = category.Name,
                Type = category.Type
            };

            return (true, "Success", editCategoryModel);
        }
    }
}
