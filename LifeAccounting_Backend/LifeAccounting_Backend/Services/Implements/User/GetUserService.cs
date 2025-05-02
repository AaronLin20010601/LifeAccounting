using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.DTOs.User;
using LifeAccounting_Backend.Services.Interfaces.User;

namespace LifeAccounting_Backend.Services.Implements.User
{
    public class GetUserService : IGetUserService
    {
        private readonly LifeAccountingDbContext _context;

        public GetUserService(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // 回傳使用者資料
        public async Task<object> GetUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            // 回傳使用者資料
            var userModel = new UserDTO
            {
                Username = user.Username,
                Email = user.Email,
                IsSync = user.IsSync,
            };

            // 包裝回傳格式
            var result = new { item = userModel };

            return result;
        }
    }
}
