using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Category
{
    public class CategoryEditDTO
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Category name must be between 1 and 30 characters.")]
        public string Name { get; set; } = null!; // 類型名稱
        [Required(ErrorMessage = "Category type is required.")]
        public string Type { get; set; } = null!; // 收支類型
    }
}
