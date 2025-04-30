using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Account
{
    // 編輯帳戶
    public class AccountEditDTO
    {
        [Required(ErrorMessage = "Account name is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Account name must be between 1 and 30 characters.")]
        public string Name { get; set; } = null!; // 帳戶名稱
        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Currency name must be between 1 and 10 characters.")]
        public string Currency { get; set; } = "TWD"; // 幣種
        [Required(ErrorMessage = "Balance is required.")]
        public decimal Balance { get; set; } // 餘額
    }
}
