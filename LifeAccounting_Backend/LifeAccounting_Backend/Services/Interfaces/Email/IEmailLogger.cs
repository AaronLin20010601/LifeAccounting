namespace LifeAccounting_Backend.Services.Interfaces.Email
{
    public interface IEmailLogger
    {
        Task LogAsync(IEnumerable<string> toEmails, string subject, string body, bool isSuccess);
    }
}
