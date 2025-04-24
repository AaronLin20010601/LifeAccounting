using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface ICreateRecordService
    {
        Task<(bool Success, string Message)> CreateRecordAsync(int userId, RecordEditDTO model);
    }
}
