namespace LifeAccounting_Backend.Services.Interfaces.Token
{
    public interface IJwtTokenService
    {
        string CreateJwtToken(Models.Entities.User user);
    }
}
