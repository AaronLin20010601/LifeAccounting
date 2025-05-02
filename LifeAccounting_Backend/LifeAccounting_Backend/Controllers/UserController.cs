using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.User;
using LifeAccounting_Backend.Services.Interfaces.User;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IGetUserService _getUserService;
        private readonly ISyncBalanceService _syncBalanceService;

        public UserController(IGetUserService getUserService, ISyncBalanceService syncBalanceService)
        {
            _getUserService = getUserService;
            _syncBalanceService = syncBalanceService;
        }

        // 取得登入使用者的資料
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var data = await _getUserService.GetUserAsync(userId);
            return Ok(data);
        }

        // 更改帳戶餘額與收支紀錄同步狀態
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> ChangeSyncBalance([FromBody] SyncBalanceDTO model)
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var success = await _syncBalanceService.SyncBalanceAsync(userId, model.IsSync);
            return success ? NoContent() : NotFound();
        }
    }
}
