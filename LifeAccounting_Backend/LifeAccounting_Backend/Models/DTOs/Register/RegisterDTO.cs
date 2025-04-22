using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Register
{
    // 取得註冊輸入資訊
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // 用戶的 Email
        [Required(ErrorMessage = "Verification code is required.")]
        public string VerificationCode { get; set; } = null!; // 用戶輸入的驗證碼
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters.")]
        public string Username { get; set; } = null!; // 用戶名稱
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters.")]
        public string Password { get; set; } = null!; // 密碼
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; } = null!; // 確認密碼
    }
}
