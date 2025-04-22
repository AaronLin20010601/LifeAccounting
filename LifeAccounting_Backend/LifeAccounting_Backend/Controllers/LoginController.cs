using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Login;
using LifeAccounting_Backend.Services.Interfaces.Login;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // 使用者登入
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return ModelStateErrorResponse();
            }

            var result = await _loginService.LoginAsync(model);
            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            // 登入成功
            return Ok(new
            {
                message = result.Message,
                token = result.Token,
                user = new
                {
                    result.User!.Id,
                    result.User.Username,
                    result.User.Email
                }
            });
        }
    }
}
