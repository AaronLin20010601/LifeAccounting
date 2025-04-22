using LifeAccounting_Backend.Models.Entities;

namespace LifeAccounting_Backend.Services.Interfaces.Token
{
    public interface IJwtTokenService
    {
        string CreateJwtToken(User user);
    }
}
