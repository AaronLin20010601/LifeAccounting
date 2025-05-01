using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Services.Interfaces.Meta;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : BaseApiController
    {
        private readonly IMetaService _recordMetaService;

        public MetaController(IMetaService recordMetaService)
        {
            _recordMetaService = recordMetaService;
        }

        // 取得登入使用者的帳戶和收支紀錄選單
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMetaData()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var (accounts, categories) = await _recordMetaService.GetMetaDataAsync(userId);
            return Ok(new
            {
                Accounts = accounts,
                Categories = categories
            });
        }
    }
}
