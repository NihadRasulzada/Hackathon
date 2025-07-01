using Application.Abstractions.Token;
using Application.Settings;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly TokenSettings _tokenSettings;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IOptions<TokenSettings> options, UserManager<AppUser> userManager)
        {
            _tokenSettings = options.Value;
            _userManager = userManager;
        }

        public async Task<Application.DTOs.Token> CreateAccessToken(AppUser user)
        {
            DateTime accessTokenExpiratin = DateTime.UtcNow
                .AddSeconds(_tokenSettings.AccessTokenExpiration);
            DateTime refreshTokenExpiratin = DateTime.UtcNow
                .AddMinutes(_tokenSettings.RefreshTokenExpiration);
            SecurityKey securityKey = SignService.GetSymmetricSecurityKey(_tokenSettings.SecurityKey);

            SigningCredentials credentials = new(securityKey,
                SecurityAlgorithms.HmacSha256Signature);


            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                expires: accessTokenExpiratin,
                notBefore: DateTime.UtcNow,
                claims: GetClaims(user, _tokenSettings.Audience).Result,
                signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(securityToken);

            Application.DTOs.Token dto = new()
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                Expiration = accessTokenExpiratin.AddHours(4),
            };

            return dto;
        }

        private async Task<IEnumerable<Claim>> GetClaims(AppUser appUser, List<string> audiences)
        {
            var userRoles = await _userManager.GetRolesAsync(appUser);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            claims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            return claims;
        }

        private string CreateRefreshToken()
        {
            Byte[] bytes = new Byte[32];
            using RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
