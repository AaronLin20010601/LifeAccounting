using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Account
{
    // 取得帳戶
    public class AccountDTO
    {
        public int Id { get; set; } // 主鍵
        [Required(ErrorMessage = "Account name is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Account name must be between 1 and 30 characters.")]
        public string Name { get; set; } = null!; // 帳戶名稱
        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Currency name must be between 1 and 10 characters.")]
        public string Currency { get; set; } = "TWD"; // 幣種
        [Required(ErrorMessage = "Create date is required.")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 建立時間
    }
}
