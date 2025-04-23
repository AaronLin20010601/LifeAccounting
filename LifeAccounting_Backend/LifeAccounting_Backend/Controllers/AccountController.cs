using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Account;
using LifeAccounting_Backend.Services.Interfaces.Account;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IGetAccountsService _getAccountsService;
        private readonly ICreateAccountService _createAccountService;
        private readonly IGetEditAccountService _getEditAccountService;
        private readonly IEditAccountService _editAccountService;
        private readonly IDeleteAccountService _deleteAccountService;

        public AccountController(
            IGetAccountsService getAccountsService, 
            ICreateAccountService createAccountService, 
            IGetEditAccountService getEditAccountService,
            IEditAccountService editAccountService,
            IDeleteAccountService deleteAccountService) 
        { 
            _getAccountsService = getAccountsService;
            _createAccountService = createAccountService;
            _getEditAccountService = getEditAccountService;
            _editAccountService = editAccountService;
            _deleteAccountService = deleteAccountService;
        }

        // 取得登入使用者的帳戶列表
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var data = await _getAccountsService.GetAccountsAsync(userId);
            return Ok(data);
        }

        // 新增帳戶
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountEditDTO model)
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

            var result = await _createAccountService.CreateAccountAsync(userId, model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 取得要被編輯的帳戶
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditAccount(int id)
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

            var result = await _getEditAccountService.GetEditAccountAsync(userId, id);
            return result.Success ? Ok(result.Data) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }

        // 編輯帳戶
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditAccount(int id, [FromBody] AccountEditDTO model)
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

            var result = await _editAccountService.EditAccountAsync(userId, id, model);
            return result.Success ? Ok(result.Message) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }

        // 刪除帳戶
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
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

            var result = await _deleteAccountService.DeleteAccountAsync(userId, id);
            return result.Success ? Ok(result.Message) : (result.Forbidden ? Forbid(result.Message) : NotFound(new { Message = result.Message }));
        }
    }
}
