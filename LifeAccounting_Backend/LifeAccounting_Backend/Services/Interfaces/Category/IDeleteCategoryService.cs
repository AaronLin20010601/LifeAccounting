namespace LifeAccounting_Backend.Services.Interfaces.Category
{
    public interface IDeleteCategoryService
    {
        Task<(bool Success, bool Forbidden, string Message)> DeleteCategoryAsync(int userId, int categoryId);
    }
}
