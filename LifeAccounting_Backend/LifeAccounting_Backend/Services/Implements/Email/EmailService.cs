using LifeAccounting_Backend.Services.Interfaces.Email;

namespace LifeAccounting_Backend.Services.Implements.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailLogger _emailLogger;

        public EmailService(IEmailSender sender, IEmailLogger logger)
        {
            _emailSender = sender;
            _emailLogger = logger;
        }

        // 發送 email
        public async Task<bool> SendEmailAsync(IEnumerable<string> toEmails, string subject, string body)
        {
            var success = await _emailSender.SendAsync(toEmails, subject, $"<p>{body}</p>", body);
            await _emailLogger.LogAsync(toEmails, subject, body, success);
            return success;
        }
    }
}
