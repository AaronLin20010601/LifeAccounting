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
        private readonly IRecordMetaService _recordMetaService;
        private readonly IGetRecordsService _getRecordsService;
        private readonly ICreateRecordService _createRecordService;
        private readonly IGetEditRecordService _getEditRecordService;
        private readonly IEditRecordService _editRecordService;
        private readonly IDeleteRecordService _deleteRecordService;

        public RecordController(
            IRecordMetaService recordMetaService, 
            IGetRecordsService getRecordsService, 
            ICreateRecordService createRecordService,
            IGetEditRecordService getEditRecordService,
            IEditRecordService editRecordService,
            IDeleteRecordService deleteRecordService)
        {
            _recordMetaService = recordMetaService;
            _getRecordsService = getRecordsService;
            _createRecordService = createRecordService;
            _getEditRecordService = getEditRecordService;
            _editRecordService = editRecordService;
            _deleteRecordService = deleteRecordService;
        }

        // 取得登入使用者的帳戶和收支紀錄選單
        [Authorize]
        [HttpGet("meta")]
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

        // 新增收支紀錄
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] RecordEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _createRecordService.CreateRecordAsync(userId, model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 取得要被編輯的收支紀錄
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditRecord(int id)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _getEditRecordService.GetEditRecordAsync(userId, id);
            return result.Success ? Ok(result.Data) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }

        // 編輯收支紀錄
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditRecord(int id, [FromBody] RecordEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _editRecordService.EditRecordAsync(userId, id, model);
            return result.Success ? Ok(result.Message) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }

        // 刪除收支紀錄
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var result = await _deleteRecordService.DeleteRecordAsync(userId, id);
            return result.Success ? Ok(result.Message) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }
    }
}
