namespace LifeAccounting_Backend.Models.DTOs.Currency
{
    public class CurrencyResponseDTO
    {
        public Dictionary<string, CurrencyRateDTO> Data { get; set; } = new(); // 幣值轉換回傳
    }
}
