using LifeAccounting_Backend.Models.DTOs.Account;

namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface IGetEditAccountService
    {
        Task<(bool Success, string Message, AccountEditDTO? Data)> GetEditAccountAsync(int userId, int accountId);
    }
}
