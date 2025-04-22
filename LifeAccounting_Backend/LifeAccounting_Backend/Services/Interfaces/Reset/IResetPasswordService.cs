using LifeAccounting_Backend.Models.DTOs.Reset;

namespace LifeAccounting_Backend.Services.Interfaces.Reset
{
    public interface IResetPasswordService
    {
        Task<(bool Success, string Message)> ResetPasswordAsync(ResetDTO model);
    }
}
