namespace LifeAccounting_Backend.Models.DTOs.Currency
{
    public class CurrencyRateDTO
    {
        public string Code { get; set; } = string.Empty; // 幣種代碼
        public decimal Value { get; set; } // 轉換幣值
    }
}
