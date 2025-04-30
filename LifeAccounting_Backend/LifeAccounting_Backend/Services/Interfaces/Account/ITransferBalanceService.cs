using LifeAccounting_Backend.Models.DTOs.Account;

namespace LifeAccounting_Backend.Services.Interfaces.Account
{
    public interface ITransferBalanceService
    {
        Task<(bool Success, string Message)> TransferBalanceAsync(int userId, TransferDTO model);
    }
}
