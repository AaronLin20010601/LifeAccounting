namespace LifeAccounting_Backend.Services.Interfaces.User
{
    public interface IGetUserService
    {
        Task<object> GetUserAsync(int userId);
    }
}
