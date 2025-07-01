using Application.Abstractions.Services;
using Application.DTOs.UserServiceDTOs.Responses;
using Application.Enums;
using Application.Helpers;
using Application.Repositories.CustomerRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.DTOs.UserServiceDTOs;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly IConfiguration _configuration;
        readonly IMailService _mailService;
        readonly ICustomerWriteRepository _customerWriteRepository;

        public UserService(UserManager<AppUser> userManager, IConfiguration configuration, IMailService mailService, ICustomerWriteRepository customerWriteRepository, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _customerWriteRepository = customerWriteRepository;
            _roleManager = roleManager;
        }

        public async Task<Response> CreateAsync(CreateUserUserServiceDTOs dto)
        {
            AppUser user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.Username,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                FinCode = dto.FinCode,
                PhoneNumber = dto.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);


            await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());


            if (result.Succeeded)
            {
                await _customerWriteRepository.AddAsync(new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    FinCode = dto.FinCode,
                    FullName = $"{dto.Name} {dto.Surname}",
                    IsDeleted = false,

                    PhoneNumber = dto.PhoneNumber,

                });

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = token.UrlEncode();
                var confirmationLink = $"{_configuration["FrontClientUrl"]}/confirm-email/{user.Id}/{token}";
                await _mailService.SendMailAsync(dto.Email, "Please confirm your email", $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>");
                await _customerWriteRepository.SaveAsync();
                return new Response(ResponseStatusCode.Success, "User created successfully. Please check your email to confirm your account.");
            }
            else
            {
                List<CustomError> errors = new List<CustomError>();
                foreach (var item in result.Errors)
                {
                    errors.Add(new CustomError
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                return new Response(ResponseStatusCode.Error, errors);
            }
        }
        public async Task<Response> UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(Convert.ToDouble(_configuration["TokenOption:RefreshTokenExpiration"]));
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new Response(ResponseStatusCode.Success, "Refresh token updated successfully.");
                }
                else
                {
                    List<CustomError> errors = new List<CustomError>();
                    foreach (var item in result.Errors)
                    {
                        errors.Add(new CustomError
                        {
                            Code = item.Code,
                            Description = item.Description
                        });
                    }
                    return new Response(ResponseStatusCode.Error, errors);
                }
            }
            else
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }
        }
        public async Task<Response> UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    result = await _userManager.UpdateSecurityStampAsync(user);
                    if (result.Succeeded)
                    {
                        return new Response(ResponseStatusCode.Success);
                    }
                    else
                    {
                        return new Response(ResponseStatusCode.Error);
                    }
                }
                else
                {
                    List<CustomError> errors = new List<CustomError>();
                    foreach (var item in result.Errors)
                    {
                        errors.Add(new CustomError
                        {
                            Code = item.Code,
                            Description = item.Description
                        });
                    }
                    return new Response(ResponseStatusCode.Error, errors);
                }
            }
            return new Response(ResponseStatusCode.NotFound, "User not found.");
        }

        public async Task<Response> DeleteAllUsersAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return new Response(ResponseStatusCode.NotFound, "No users found to delete.");
            }
            foreach (var user in users)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    List<CustomError> errors = new List<CustomError>();
                    foreach (var item in result.Errors)
                    {
                        errors.Add(new CustomError
                        {
                            Code = item.Code,
                            Description = item.Description
                        });
                    }
                    return new Response(ResponseStatusCode.Error, errors);
                }
            }
            return new Response(ResponseStatusCode.Success, "All users deleted successfully.");
        }

        public async Task<Response<GetAllUsersUserSeviceResponseDTOs>> GetAllUsersAsync(int page, int size)
        {
            List<ListUser>? users = await _userManager.Users
                  .Skip(page * size)
                  .Take(size)
                  .Select(user => new ListUser
                  {
                      Id = user.Id.ToString(),
                      Email = user.Email,
                      Name = user.Name,
                      Surname = user.Surname,
                      UserName = user.UserName,
                      TwoFactorEnabled = user.TwoFactorEnabled,
                  })
                  .ToListAsync();

            if (users == null || !users.Any())
            {
                return new Response<GetAllUsersUserSeviceResponseDTOs>(ResponseStatusCode.NotFound, "No users found.");
            }
            return new Response<GetAllUsersUserSeviceResponseDTOs>(ResponseStatusCode.Success, new GetAllUsersUserSeviceResponseDTOs
            {
                Users = users,
                TotalUsersCount = TotalUsersCount
            });
        }

        public async Task<Response> AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
                return new Response(ResponseStatusCode.Success, "Roles assigned successfully.");
            }
            else
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }
        }
        public async Task<Response<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser? user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return new Response<string[]>(ResponseStatusCode.Success, userRoles.ToArray());
            }
            return new Response<string[]>(ResponseStatusCode.NotFound, "User not found.");
        }

        public int TotalUsersCount => _userManager.Users.Count();
    }
}
