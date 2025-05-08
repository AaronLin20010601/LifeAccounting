namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IDeleteRecordService
    {
        Task<(bool Success, string Message)> DeleteRecordAsync(int userId, int recordId);
    }
}
