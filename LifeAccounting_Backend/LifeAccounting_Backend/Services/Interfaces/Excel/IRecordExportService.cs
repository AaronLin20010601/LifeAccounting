using Microsoft.AspNetCore.Mvc;

namespace LifeAccounting_Backend.Services.Interfaces.Excel
{
    public interface IRecordExportService
    {
        Task<FileContentResult> GenerateExcelAsync(int userId, int? accountId, int? categoryId, string? type, DateTime? startDate, DateTime? endDate);
    }
}
