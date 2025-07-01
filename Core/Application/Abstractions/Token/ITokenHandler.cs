using Domain.Entities.Identity;

namespace Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<DTOs.Token> CreateAccessToken(AppUser appUser);
    }
}
