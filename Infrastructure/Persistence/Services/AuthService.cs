using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.DTOs;
using Application.DTOs.AuthServiceDTOs.Response;
using Application.Helpers;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;
        readonly IOtpService _otpService;
        readonly AppDbContext _context;
        readonly IConfiguration _configuration;
        public AuthService(UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService,
            IMailService mailService,
            IOtpService otpService,
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
            _otpService = otpService;
            _context = context;
            _configuration = configuration;
        }
        public async Task<Response<LoginResponseDto>> LoginAsync(string usernameOrEmail, string password)
        {
            AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                return new Response<LoginResponseDto>(ResponseStatusCode.NotFound, "The username or password is incorrect.");

            if (await _userManager.IsInRoleAsync(user, _configuration.GetValue<string>("SuperAdmin:Role")))
            {
                if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    return new Response<LoginResponseDto>(ResponseStatusCode.ValidationError, "Super Admin user does not have a password set. Please reset the password.");
                }
            }

            if (!user.EmailConfirmed)
            {
                return new Response<LoginResponseDto>(ResponseStatusCode.ValidationError, "Email is not confirmed. Please check your email for confirmation link.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
            {
                return new Response<LoginResponseDto>(ResponseStatusCode.NotFound, "The username or password is incorrect.");
            }

            if (user.TwoFactorEnabled)
            {
                LoginAuthServiceResponseDto dto = new LoginAuthServiceResponseDto()
                {
                    IsEmailVerified = user.IsEmailVerified,
                    IsPhoneVerified = user.IsPhoneVerified,
                    TwoFactorRequired = true
                };
                return new Response<LoginResponseDto>(ResponseStatusCode.Success, new LoginResponseDto() { LoginInfo = dto });
            }
            else
            {
                if (result.Succeeded)
                {
                    Token? token = await _tokenHandler.CreateAccessToken(user);
                    if (token == null)
                        return new Response<LoginResponseDto>(ResponseStatusCode.Error, "Token creation failed.");
                    Response response = await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
                    return new Response<LoginResponseDto>(response, new LoginResponseDto() { Token = token });
                }
                else if (result.IsLockedOut)
                {
                    return new Response<LoginResponseDto>(ResponseStatusCode.ValidationError, "Your account is locked out. Please try again later.");
                }
                else
                {
                    return new Response<LoginResponseDto>(ResponseStatusCode.NotFound, "The username or password is incorrect.");
                }
            }
        }

        public async Task<Response<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow.AddHours(4))
            {
                Token? token = await _tokenHandler.CreateAccessToken(user);
                if (token == null)
                    return new Response<Token>(ResponseStatusCode.Error, "Token creation failed.");
                Response response = await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
                return new Response<Token>(response, token);
            }
            else
                return new Response<Token>(ResponseStatusCode.NotFound, "The refresh token is invalid or expired.");
        }

        public async Task<Response> EmailPasswordResetAsnyc(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new Response(ResponseStatusCode.ValidationError, "Email cannot be null or empty.");
            }
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string? resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                if (string.IsNullOrEmpty(resetToken))
                {
                    return new Response(ResponseStatusCode.Error, "Failed to generate password reset token.");
                }

                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
                return new Response(ResponseStatusCode.Success, "Password reset email has been sent successfully.");
            }
            else
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided email.");
            }
        }

        public async Task<Response> EmailVerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                bool b = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
                if (b)
                {
                    return new Response(ResponseStatusCode.Success, "Reset token is valid.");
                }
                else
                {
                    return new Response(ResponseStatusCode.ValidationError, "Reset token is invalid or expired.");
                }
            }
            else
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided ID.");
            }
        }

        public async Task<Response> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return new Response(ResponseStatusCode.ValidationError, "User ID and token cannot be null or empty.");
            }

            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");

            }

            token = token.UrlDecode();

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new Response(ResponseStatusCode.Success, "Email confirmed successfully.");
            }
            else
            {
                List<CustomError> errors = new List<CustomError>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new CustomError
                    {
                        Code = error.Code,
                        Description = error.Description
                    });
                }
                return new Response(ResponseStatusCode.Error, errors);
            }
        }

        public async Task<Response> AddPhoneNumberAsync(string number, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }

            user.PhoneNumber = number;

            string? otp = await _otpService.SendOtpNumberAsync(number);

            if (string.IsNullOrEmpty(otp))
            {
                return new Response(ResponseStatusCode.Error, "Failed to send OTP.");
            }

            var OtpEntry = new AppUserOtp
            {
                UserId = user.Id,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddHours(4).AddMinutes(5)
            };

            await _context.AppUserOtps.AddAsync(OtpEntry);
            await _context.SaveChangesAsync();
            return new Response(ResponseStatusCode.Success, "OTP sent successfully. Please verify your number.");

        }

        public async Task<Response> VerifyPhoneNumberAsync(string userName, string otp)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }

            AppUserOtp? otpEntry = await _context.AppUserOtps
                .Where(o => o.UserId == user.Id && o.Otp == otp)
                .FirstOrDefaultAsync();

            if (otpEntry == null)
            {
                return new Response(ResponseStatusCode.ValidationError, "Invalid OTP.");
            }

            if (otpEntry.ExpirationTime < DateTime.UtcNow.AddHours(4))
            {
                return new Response(ResponseStatusCode.ValidationError, "OTP has expired.");
            }

            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
            _context.AppUserOtps.Remove(otpEntry);
            await _context.SaveChangesAsync();
            return new Response(ResponseStatusCode.Success, "Phone number verified successfully.");
        }

        public async Task<Response> LogoutAsync(string userName, string refreshToken)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(refreshToken))
            {
                return new Response(ResponseStatusCode.ValidationError, "User ID and refresh token cannot be null or empty.");
            }
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.RefreshToken == refreshToken);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found or refresh token is invalid.");
            }
            user.RefreshToken = null;
            user.RefreshTokenEndDate = null;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new Response(ResponseStatusCode.Success, "Logout successful.");
            }
            else
            {
                return new Response(ResponseStatusCode.Error, "Failed to logout.");
            }
        }

        public async Task<Response> CheckNumberVerifiedAsync(string userName)
        {
            var isVerified = await _userManager.Users
                .Where(u => u.UserName == userName && u.PhoneNumberConfirmed)
                .AnyAsync();

            if (isVerified)
            {
                return new Response(ResponseStatusCode.Success, "Phone number is verified.");
            }
            else
            {
                return new Response(ResponseStatusCode.ValidationError, "Phone number is not verified.");
            }
        }

        public async Task<Response> CheckEmailVerifyAsync(string userName)
        {
            var isVerified = await _userManager.Users
                .Where(u => u.EmailConfirmed && u.UserName == userName)
                .AnyAsync();
            if (isVerified)
            {
                return new Response(ResponseStatusCode.Success, "Email is verified.");
            }
            else
            {
                return new Response(ResponseStatusCode.ValidationError, "Email is not verified.");
            }
        }

        public async Task<Response> LoginSendOtpToEmailAsync(string userEmail)
        {
            AppUser? user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided email.");
            }
            string? otp = await _otpService.SendOtpEmailAsync(userEmail);
            if (string.IsNullOrEmpty(otp))
            {
                return new Response(ResponseStatusCode.Error, "Failed to send OTP.");
            }
            var OtpEntry = new AppUserOtp
            {
                UserId = user.Id,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddHours(4).AddMinutes(5)
            };
            await _context.AppUserOtps.AddAsync(OtpEntry);
            await _context.SaveChangesAsync();
            return new Response(ResponseStatusCode.Success, "OTP sent successfully. Please verify your email.");
        }

        public async Task<Response<Token>> LoginVerifyOtpEmailAsync(string userEmail, string otpInput)
        {
            AppUser? user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new Response<Token>(ResponseStatusCode.NotFound, "User not found with the provided email.");
            }
            var otpEntry = await _context.AppUserOtps
                .Where(o => o.UserId == user.Id && o.Otp == otpInput)
                .FirstOrDefaultAsync();
            if (otpEntry == null)
            {
                return new Response<Token>(ResponseStatusCode.ValidationError, "Invalid OTP.");
            }
            if (otpEntry.ExpirationTime < DateTime.UtcNow.AddHours(4))
            {
                return new Response<Token>(ResponseStatusCode.ValidationError, "OTP has expired.");
            }
            Token? token = await _tokenHandler.CreateAccessToken(user);
            if (token == null)
                return new Response<Token>(ResponseStatusCode.Error, "Token creation failed.");
            Response response = await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
            return new Response<Token>(response, token);

        }

        public async Task<Response> LoginSendOtpToNumberAsync(string number)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == number);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided phone number.");
            }
            string? otp = await _otpService.SendOtpNumberAsync(number);
            if (string.IsNullOrEmpty(otp))
            {
                return new Response(ResponseStatusCode.Error, "Failed to send OTP.");
            }
            var OtpEntry = new AppUserOtp
            {
                UserId = user.Id,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddHours(4).AddMinutes(5)
            };
            await _context.AppUserOtps.AddAsync(OtpEntry);
            await _context.SaveChangesAsync();
            return new Response(ResponseStatusCode.Success, "OTP sent successfully. Please verify your phone number.");

        }

        public async Task<Response<Token>> LoginVerifyOtpNumberAsync(string number, string otpInput)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == number);
            if (user == null)
            {
                return new Response<Token>(ResponseStatusCode.NotFound, "User not found with the provided phone number.");
            }
            var otpEntry = await _context.AppUserOtps
                .Where(o => o.UserId == user.Id && o.Otp == otpInput)
                .FirstOrDefaultAsync();
            if (otpEntry == null)
            {
                return new Response<Token>(ResponseStatusCode.ValidationError, "Invalid OTP.");
            }
            if (otpEntry.ExpirationTime < DateTime.UtcNow.AddHours(4))
            {
                return new Response<Token>(ResponseStatusCode.ValidationError, "OTP has expired.");
            }
            Token? token = await _tokenHandler.CreateAccessToken(user);
            if (token == null)
                return new Response<Token>(ResponseStatusCode.Error, "Token creation failed.");
            Response response = await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration);
            return new Response<Token>(response, token);
        }

        public async Task<Response> SuperAdminVerifySetPasswordTokenAsync(string email, string token)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided email.");
            }
            if (!(await _userManager.IsInRoleAsync(user, _configuration.GetValue<string>("SuperAdmin:Role"))))
            {
                return new Response(ResponseStatusCode.ValidationError, "User is not a Super Admin.");
            }
            token = token.UrlDecode();
            bool isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (isValidToken)
            {
                return new Response(ResponseStatusCode.Success, "Reset token is valid.");
            }
            else
            {
                return new Response(ResponseStatusCode.ValidationError, "Reset token is invalid or expired.");
            }
        }

        public async Task<Response> SuperAdminSetPasswordAsync(string email, string token, string password)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found with the provided email.");
            }
            if (!(await _userManager.IsInRoleAsync(user, _configuration.GetValue<string>("SuperAdmin:Role"))))
            {
                return new Response(ResponseStatusCode.ValidationError, "User is not a Super Admin.");
            }
            token = token.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return new Response(ResponseStatusCode.Success, "Password has been set successfully.");
            }
            else
            {
                List<CustomError> errors = new List<CustomError>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new CustomError
                    {
                        Code = error.Code,
                        Description = error.Description
                    });
                }
                return new Response(ResponseStatusCode.Error, errors);
            }
        }
    }
}
