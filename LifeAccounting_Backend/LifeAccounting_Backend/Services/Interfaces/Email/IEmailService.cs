namespace LifeAccounting_Backend.Services.Interfaces.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(IEnumerable<string> toEmails, string subject, string body);
    }
}
