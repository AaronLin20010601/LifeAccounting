namespace LifeAccounting_Backend.Models.Entities
{
    // 帳戶
    public class Account
    {
        public int Id { get; set; } // 主鍵
        public int UserId { get; set; } // 使用者編號
        public string Name { get; set; } = null!; // 帳戶名稱
        public string Currency { get; set; } = "TWD"; // 幣種
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間
        public decimal Balance { get; set; } // 帳戶餘額

        public User User { get; set; } = null!;
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
