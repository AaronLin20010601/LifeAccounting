using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Verification;
using LifeAccounting_Backend.Services.Interfaces.VerifyCode;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : BaseApiController
    {
        private readonly IRegisterVerificationService _registerService;
        private readonly IVerificationCodeService _resetService;

        public VerificationController(IRegisterVerificationService registerService, IResetVerificationService resetService)
        {
            _registerService = registerService;
            _resetService = resetService;
        }

        // 註冊驗證碼
        [HttpPost("register")]
        public async Task<IActionResult> SendRegisterCode([FromBody] EmailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _registerService.SendVerificationCodeAsync(model.Email);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 重設密碼驗證碼
        [HttpPost("reset")]
        public async Task<IActionResult> SendResetCode([FromBody] EmailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _resetService.SendVerificationCodeAsync(model.Email);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }
    }
}
