using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Record
{
    public class RecordEditDTO
    {
        [Required(ErrorMessage = "Account is required.")]
        public int AccountId { get; set; } // 帳戶編號
        public int? CategoryId { get; set; } // 類型編號
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; } // 金額
        public string? Note { get; set; } // 備註
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } // 收支時間
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = null!; // 收支類型
    }
}
