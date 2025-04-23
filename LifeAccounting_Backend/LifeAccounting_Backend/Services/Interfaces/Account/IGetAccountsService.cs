namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface IGetAccountsService
    {
        Task<object> GetAccountsAsync(int userId);
    }
}
