using Application.Abstractions.Services.Authentications;
using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
        Task<Response> EmailPasswordResetAsnyc(string email);
        Task<Response> EmailVerifyResetTokenAsync(string resetToken, string userId);
        Task<Response> ConfirmEmailAsync(string userId, string token);
        Task<Response> AddPhoneNumberAsync(string number, string userName);
        Task<Response> VerifyPhoneNumberAsync(string userName, string otp);
        Task<Response> LogoutAsync(string userName, string refreshToken);
        Task<Response> CheckNumberVerifiedAsync(string userName);
        Task<Response> CheckEmailVerifyAsync(string userName);
        Task<Response> LoginSendOtpToEmailAsync(string userEmail);
        Task<Response<DTOs.Token>> LoginVerifyOtpEmailAsync(string userEmail, string otpInput);
        Task<Response> LoginSendOtpToNumberAsync(string number);
        Task<Response<DTOs.Token>> LoginVerifyOtpNumberAsync(string number, string otpInput);
        Task<Response> SuperAdminVerifySetPasswordTokenAsync(string email, string token);
        Task<Response> SuperAdminSetPasswordAsync(string email, string token, string password);
    }
}
