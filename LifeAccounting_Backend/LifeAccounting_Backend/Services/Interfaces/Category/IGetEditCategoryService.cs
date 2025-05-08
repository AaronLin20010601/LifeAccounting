using LifeAccounting_Backend.Models.DTOs.Category;

namespace LifeAccounting_Backend.Services.Interfaces.Category
{
    public interface IGetEditCategoryService
    {
        Task<(bool Success, string Message, CategoryEditDTO? Data)> GetEditCategoryAsync(int userId, int categoryId);
    }
}
