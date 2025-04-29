using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Services.Interfaces.Excel;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : BaseApiController
    {
        private readonly IRecordExportService _recordExportService;

        public ExcelController(IRecordExportService recordExportService)
        {
            _recordExportService = recordExportService;
        }

        // 下載選取範圍收支紀錄 excel
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ExportRecordsToExcel(
                [FromQuery] int? accountId, [FromQuery] int? categoryId, [FromQuery] string? type, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate
            )
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var file = await _recordExportService.GenerateExcelAsync(userId, accountId, categoryId, type, startDate, endDate);
            return file;
        }
    }
}
