namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface IDeleteAccountService
    {
        Task<(bool Success, string Message)> DeleteAccountAsync(int userId, int accountId);
    }
}
