using LifeAccounting_Backend.Models.DTOs.Login;

namespace LifeAccounting_Backend.Services.Interfaces.Login
{
    public interface ILoginService
    {
        Task<(bool Success, string Message, string? Token, Models.Entities.User? User)> LoginAsync(LoginDTO model);
    }
}
