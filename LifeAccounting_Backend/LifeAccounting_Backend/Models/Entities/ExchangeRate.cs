namespace LifeAccounting_Backend.Models.Entities
{
    // 匯率對資料
    public class ExchangeRate
    {
        public int Id { get; set; } // 主鍵
        public string FromCurrency { get; set; } = null!; // 原貨幣
        public string ToCurrency { get; set; } = null!; // 轉換貨幣
        public decimal ToPrice { get; set; } // 轉換貨幣值
        public DateTime UpdatedAt { get; set; } // 更新時間
    }
}
