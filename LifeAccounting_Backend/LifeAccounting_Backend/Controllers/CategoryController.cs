using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeAccounting_Backend.Models.DTOs.Category;
using LifeAccounting_Backend.Services.Interfaces.Category;

namespace LifeAccounting_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly IGetCategoriesService _getCategoriesService;
        private readonly ICreateCategoryService _createCategoryService;
        private readonly IGetEditCategoryService _getEditCategoryService;
        private readonly IEditCategoryService _editCategoryService;
        private readonly IDeleteCategoryService _deleteCategoryService;

        public CategoryController(
            IGetCategoriesService getCategoriesService,
            ICreateCategoryService createCategoryService,
            IGetEditCategoryService getEditCategoryService,
            IEditCategoryService editCategoryService,
            IDeleteCategoryService deleteCategoryService)
        {
            _getCategoriesService = getCategoriesService;
            _createCategoryService = createCategoryService;
            _getEditCategoryService = getEditCategoryService;
            _editCategoryService = editCategoryService;
            _deleteCategoryService = deleteCategoryService;
        }

        // 取得登入使用者的收支類型列表
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            // 確保有登入的用戶
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var data = await _getCategoriesService.GetCategoriesAsync(userId);
            return Ok(data);
        }

        // 新增收支類型
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryEditDTO model)
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

            var result = await _createCategoryService.CreateCategoryAsync(userId, model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 取得要被編輯的收支類型
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditCategory(int id)
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

            var result = await _getEditCategoryService.GetEditCategoryAsync(userId, id);
            return result.Success ? Ok(result.Data) : BadRequest(new { Message = result.Message });
        }

        // 編輯收支類型
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryEditDTO model)
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

            var result = await _editCategoryService.EditCategoryAsync(userId, id, model);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }

        // 刪除收支類型
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
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

            var result = await _deleteCategoryService.DeleteCategoryAsync(userId, id);
            return result.Success ? Ok(result.Message) : BadRequest(new { Message = result.Message });
        }
    }
}
