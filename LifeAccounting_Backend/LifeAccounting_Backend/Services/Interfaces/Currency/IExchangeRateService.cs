namespace LifeAccounting_Backend.Services.Interfaces.Currency
{
    public interface IExchangeRateService
    {
        Task SeedExchangeRatesAsync();
    }
}
