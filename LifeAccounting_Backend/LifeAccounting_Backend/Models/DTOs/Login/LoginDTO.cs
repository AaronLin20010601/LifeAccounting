using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Login
{
    // 取得登入輸入資訊
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!; // email
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!; // 密碼
    }
}
