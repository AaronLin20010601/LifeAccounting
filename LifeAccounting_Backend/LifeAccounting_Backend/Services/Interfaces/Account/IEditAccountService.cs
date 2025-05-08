using LifeAccounting_Backend.Models.DTOs.Account;

namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface IEditAccountService
    {
        Task<(bool Success, string Message)> EditAccountAsync(int userId, int accountId, AccountEditDTO model);
    }
}
