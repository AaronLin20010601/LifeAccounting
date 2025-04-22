namespace LifeAccounting_Backend.Services.Interfaces.VerifyCode
{
    public interface IVerificationCodeService
    {
        Task<(bool Success, string Message)> SendVerificationCodeAsync(string email);
    }

    public interface IRegisterVerificationService : IVerificationCodeService { }
    public interface IResetVerificationService : IVerificationCodeService { }
}
