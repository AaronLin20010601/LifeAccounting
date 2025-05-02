namespace LifeAccounting_Backend.Models.DTOs.User
{
    public class UserDTO
    {
        public string Username { get; set; } = null!; // 用戶名
        public string Email { get; set; } = null!; // email
        public bool IsSync { get; set; } = false; // 帳戶同步紀錄
    }
}
