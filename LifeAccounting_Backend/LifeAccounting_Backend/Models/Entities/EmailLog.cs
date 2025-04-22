namespace LifeAccounting_Backend.Models.Entities
{
    // Email 傳送紀錄
    public class EmailLog
    {
        public int Id { get; set; } // 主鍵
        public string ToEmail { get; set; } = null!; // 目標email
        public string Subject { get; set; } = null!; // 郵件主題
        public string Body { get; set; } = null!; // 郵件內容
        public DateTime SentAt { get; set; } = DateTime.UtcNow; // 發送時間
        public bool IsSuccess { get; set; } // 發送結果
    }
}
