namespace LifeAccounting_Backend.Models.Entities
{
    // 註冊和重設密碼驗證碼
    public class ResetToken
    {
        public int Id { get; set; } // 主鍵
        public string Token { get; set; } = null!; // 驗證碼
        public string Email { get; set; } = null!; // email
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間
        public DateTime ExpirationDate { get; set; } // 過期時間
        public bool IsUsed { get; set; } = false; // 是否已使用過
    }
}
