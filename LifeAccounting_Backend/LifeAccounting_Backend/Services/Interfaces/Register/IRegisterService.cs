using LifeAccounting_Backend.Models.DTOs.Register;

namespace LifeAccounting_Backend.Services.Interfaces.Register
{
    public interface IRegisterService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDTO model);
    }
}
