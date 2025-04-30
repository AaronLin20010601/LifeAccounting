using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Account
{
    public class TransferDTO
    {
        [Required(ErrorMessage = "Account is required.")]
        public int FromAccountId { get; set; }
        [Required(ErrorMessage = "Account is required.")]
        public int ToAccountId { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }
    }
}
