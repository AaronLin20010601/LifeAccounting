namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IGetRecordsService
    {
        Task<object> GetRecordsAsync(int userId, int? accountId, int? categoryId, string? type, DateTime? startDate, DateTime? endDate, int page, int pageSize);
    }
}
