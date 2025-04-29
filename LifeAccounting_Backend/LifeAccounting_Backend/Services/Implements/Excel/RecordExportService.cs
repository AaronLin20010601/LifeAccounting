using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LifeAccounting_Backend.Services.Interfaces.Excel;
using LifeAccounting_Backend.Services.Interfaces.Record;
using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Services.Implements.Excel
{
    public class RecordExportService : IRecordExportService
    {
        private readonly IGetRecordsService _getRecordsService;

        public RecordExportService(IGetRecordsService getRecordsService)
        {
            _getRecordsService = getRecordsService;
        }

        // 匯出查詢收支紀錄 excel
        public async Task<FileContentResult> GenerateExcelAsync(int userId, int? accountId, int? categoryId, string? type, DateTime? startDate, DateTime? endDate)
        {

            var result = await _getRecordsService.GetRecordsAsync(userId, accountId, categoryId, type, startDate, endDate, 1, int.MaxValue);
            var json = JsonSerializer.Serialize(result);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            var itemsJson = dictionary["items"].GetRawText();
            var items = JsonSerializer.Deserialize<List<RecordDTO>>(itemsJson);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Records");

            // 標題列
            worksheet.Cell(1, 1).Value = "Account";
            worksheet.Cell(1, 2).Value = "Category";
            worksheet.Cell(1, 3).Value = "Amount";
            worksheet.Cell(1, 4).Value = "Note";
            worksheet.Cell(1, 5).Value = "Date";
            worksheet.Cell(1, 6).Value = "Type";

            int row = 2;
            foreach (var r in items)
            {
                worksheet.Cell(row, 1).Value = r.AccountName;
                worksheet.Cell(row, 2).Value = r.CategoryName;
                worksheet.Cell(row, 3).Value = r.Amount;
                worksheet.Cell(row, 4).Value = r.Note;
                worksheet.Cell(row, 5).Value = r.Date.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 6).Value = r.Type;
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return new FileContentResult(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"records_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            };
        }
    }
}
