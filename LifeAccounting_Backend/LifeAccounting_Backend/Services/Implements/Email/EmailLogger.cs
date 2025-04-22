using LifeAccounting_Backend.Models;
using LifeAccounting_Backend.Models.Entities;
using LifeAccounting_Backend.Services.Interfaces.Email;

namespace LifeAccounting_Backend.Services.Implements.Email
{
    public class EmailLogger : IEmailLogger
    {
        private readonly LifeAccountingDbContext _context;

        public EmailLogger(LifeAccountingDbContext context)
        {
            _context = context;
        }

        // email 傳送紀錄
        public async Task LogAsync(IEnumerable<string> toEmails, string subject, string body, bool isSuccess)
        {
            // 將郵件記錄存入資料庫
            var emailLog = new EmailLog
            {
                ToEmail = string.Join(",", toEmails),
                Subject = subject,
                Body = body,
                IsSuccess = isSuccess,
                SentAt = DateTime.UtcNow
            };

            _context.EmailLogs.Add(emailLog);
            await _context.SaveChangesAsync();
        }
    }
}
