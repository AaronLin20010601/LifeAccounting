using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IEditRecordService
    {
        Task<(bool Success, bool Forbidden, string Message)> EditRecordAsync(int userId, int recordId, RecordEditDTO model);
    }
}
