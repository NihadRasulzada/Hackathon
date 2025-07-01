using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface ITwoFactorAuthenticationService
    {
        Task<Response> EnableTwoFactorAuthenticationAsync(string userName);
        Task<Response> DisableTwoFactorAuthenticationAsync(string userName);
    }
}
