using Application.DTOs.AuthServiceDTOs.Response;
using Application.ResponceObject;

namespace Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Response<LoginResponseDto>> LoginAsync(string usernameOrEmail, string password);
        Task<Response<DTOs.Token>> RefreshTokenLoginAsync(string refreshToken);
    }
}
