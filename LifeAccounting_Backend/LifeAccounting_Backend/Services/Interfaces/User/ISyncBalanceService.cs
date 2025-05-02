namespace LifeAccounting_Backend.Services.Interfaces.User
{
    public interface ISyncBalanceService
    {
        Task<bool> SyncBalanceAsync(int userId, bool isSync);
    }
}
