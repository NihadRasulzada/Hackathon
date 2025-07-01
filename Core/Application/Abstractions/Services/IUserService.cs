using Application.DTOs.UserServiceDTOs.Responses;
using Application.ResponceObject;
using Domain.Entities.Identity;
using Persistence.DTOs.UserServiceDTOs;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<Response> CreateAsync(CreateUserUserServiceDTOs dto);
        Task<Response> UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate);
        Task<Response> UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<Response<GetAllUsersUserSeviceResponseDTOs>> GetAllUsersAsync(int page, int size);
        Task<Response> DeleteAllUsersAsync();
        int TotalUsersCount { get; }
        Task<Response> AssignRoleToUserAsnyc(string userId, string[] roles);
        Task<Response<string[]>> GetRolesToUserAsync(string userIdOrName);
        Task<Response> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
