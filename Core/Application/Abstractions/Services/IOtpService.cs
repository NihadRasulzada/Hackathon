namespace Application.Abstractions.Services
{
    public interface IOtpService
    {
        Task<string> SendOtpNumberAsync(string phoneNumber);
        Task<string> SendOtpEmailAsync(string email);
    }
}
