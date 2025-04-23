using LifeAccounting_Backend.Models.DTOs.Category;

namespace LifeAccounting_Backend.Services.Interfaces.Category
{
    public interface ICreateCategoryService
    {
        Task<(bool Success, string Message)> CreateCategoryAsync(int userId, CategoryEditDTO model);
    }
}
