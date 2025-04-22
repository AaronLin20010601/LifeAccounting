namespace LifeAccounting_Backend.Models.Entities
{
    // 收支紀錄
    public class Record
    {
        public int Id { get; set; } // 主鍵
        public int UserId { get; set; } // 使用者編號
        public int AccountId { get; set; } // 帳戶編號
        public int? CategoryId { get; set; } // 類型編號
        public decimal Amount { get; set; } // 金額
        public string? Note { get; set; } // 備註
        public DateTime Date { get; set; } // 收支時間
        public string Type { get; set; } = null!; // 收支類型
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間

        public User User { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public Category? Category { get; set; }
    }
}
