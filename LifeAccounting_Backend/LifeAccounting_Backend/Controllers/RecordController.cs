using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Record;
using LifeAccounting_Backend.Services.Interfaces.Record;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : BaseApiController
    {
        private readonly IGetRecordsService _getRecordsService;

        public RecordController(IGetRecordsService getRecordsService)
        {
            _getRecordsService = getRecordsService;
        }

        // 取得登入使用者的收支紀錄列表
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRecords(
                [FromQuery] int? accountId, [FromQuery] int? categoryId, [FromQuery] string? type, 
                [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10
            )
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var data = await _getRecordsService.GetRecordsAsync(userId, accountId, categoryId, type, startDate, endDate, page , pageSize);
            return Ok(data);
        }
    }
}
