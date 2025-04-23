namespace LifeAccounting_Backend.Services.Interfaces.Category
{
    public interface IGetCategoriesService
    {
        Task<object> GetCategoriesAsync(int userId);
    }
}
