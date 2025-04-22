using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Register;
using LifeAccounting_Backend.Services.Interfaces.Register;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : BaseApiController
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        // 註冊用戶
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _registerService.RegisterAsync(model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }
    }
}
