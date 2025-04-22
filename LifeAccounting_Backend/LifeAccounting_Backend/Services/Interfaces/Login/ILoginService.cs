using LifeAccounting_Backend.Models.Entities;
using LifeAccounting_Backend.Models.DTOs.Login;

namespace LifeAccounting_Backend.Services.Interfaces.Login
{
    public interface ILoginService
    {
        Task<(bool Success, string Message, string? Token, User? User)> LoginAsync(LoginDTO model);
    }
}
