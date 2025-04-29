using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Models.DTOs.Excel
{
    public class RecordListDTO
    {
        public List<RecordDTO> Items { get; set; } = new();
    }
}
