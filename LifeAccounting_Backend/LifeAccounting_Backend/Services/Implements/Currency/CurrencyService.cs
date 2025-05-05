using LifeAccounting_Backend.Models.DTOs.Currency;
using LifeAccounting_Backend.Services.Interfaces.Currency;

namespace LifeAccounting_Backend.Services.Implements.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CurrencyService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        // 從外部 api 取得匯率轉換資料
        public async Task<decimal> GetRateAsync(string fromCurrency, string toCurrency)
        {
            var apiKey = _config["CurrencyApi:ApiKey"];
            var url = $"https://api.currencyapi.com/v3/latest?apikey={apiKey}&base_currency={fromCurrency}&currencies={toCurrency}";

            var response = await _httpClient.GetFromJsonAsync<CurrencyResponseDTO>(url);
            if (response?.Data == null || !response.Data.ContainsKey(toCurrency))
                throw new Exception($"Rate {fromCurrency}->{toCurrency} not found.");

            return response.Data[toCurrency].Value;
        }
    }
}
