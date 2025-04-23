using LifeAccounting_Backend.Models.DTOs.Category;

namespace LifeAccounting_Backend.Services.Interfaces.Category
{
    public interface IEditCategoryService
    {
        Task<(bool Success, bool Forbidden, string Message)> EditCategoryAsync(int userId, int categoryId, CategoryEditDTO model);
    }
}
