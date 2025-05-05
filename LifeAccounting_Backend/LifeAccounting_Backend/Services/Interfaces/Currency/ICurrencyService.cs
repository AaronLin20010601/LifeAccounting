namespace LifeAccounting_Backend.Services.Interfaces.Currency
{
    public interface ICurrencyService
    {
        Task<decimal> GetRateAsync(string fromCurrency, string toCurrency);
    }
}
