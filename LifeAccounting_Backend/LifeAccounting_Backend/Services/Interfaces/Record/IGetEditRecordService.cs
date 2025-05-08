using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IGetEditRecordService
    {
        Task<(bool Success, string Message, RecordEditDTO? Data)> GetEditRecordAsync(int userId, int recordId);
    }
}
