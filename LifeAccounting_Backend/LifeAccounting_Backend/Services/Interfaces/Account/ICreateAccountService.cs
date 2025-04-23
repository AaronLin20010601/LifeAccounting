using LifeAccounting_Backend.Models.DTOs.Account;

namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface ICreateAccountService
    {
        Task<(bool Success, string Message)> CreateAccountAsync(int userId, AccountEditDTO model);
    }
}
