using System.ComponentModel.DataAnnotations;

namespace LifeAccounting_Backend.Models.DTOs.Record
{
    // 取得收支紀錄
    public class RecordDTO
    {
        public int Id { get; set; } // 主鍵
        [Required(ErrorMessage = "Account is required.")]
        public int AccountId { get; set; } // 帳戶編號
        public string AccountName { get; set; }
        public int? CategoryId { get; set; } // 類型編號
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; } // 金額
        [StringLength(2000, ErrorMessage = "Note must be shorter than 2000 characters.")]
        public string? Note { get; set; } // 備註
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } // 收支時間
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = null!; // 收支類型
    }
}
