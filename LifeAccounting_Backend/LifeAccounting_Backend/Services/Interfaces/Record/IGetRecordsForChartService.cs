namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IGetRecordsForChartService
    {
        Task<object> GetRecordsForChartAsync(int userId, int? accountId, int? categoryId, string? type, DateTime? startDate, DateTime? endDate, string? toCurrency = null);
    }
}
