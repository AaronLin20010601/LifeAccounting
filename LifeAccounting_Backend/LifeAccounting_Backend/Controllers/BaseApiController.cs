using Microsoft.AspNetCore.Mvc;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        // 共用錯誤訊息回應
        protected IActionResult ModelStateErrorResponse()
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new { Message = string.Join(", ", errors) });
        }
    }
}
