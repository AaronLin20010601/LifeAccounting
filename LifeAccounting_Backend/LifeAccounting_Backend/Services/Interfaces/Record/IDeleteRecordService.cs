namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IDeleteRecordService
    {
        Task<(bool Success, bool Forbidden, string Message)> DeleteRecordAsync(int userId, int recordId);
    }
}
