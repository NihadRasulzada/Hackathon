using Api.Extensions;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.AuthServiceDTOs;
using Application.DTOs.AuthServiceDTOs.Response;
using Application.ResponceObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;
        public AuthController(IAuthService authService, IOptions<JwtBearerOptions> jwtOptions)
        {
            _authService = authService;
            _jwtOptions = jwtOptions;
        }

        #region Login
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<LoginResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<LoginResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<LoginResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginAuthServiceDto dto)
        {
            Response<LoginResponseDto> response = await _authService.LoginAsync(dto.UsernameOrEmail, dto.Password);
            return this.HandleResponse(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginAuthServiceDto dto)
        {
            var responce = await _authService.RefreshTokenLoginAsync(dto.RefreshToken);
            return this.HandleResponse(responce);
        }
        #endregion

        [HttpPost("email-password-reset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmailPasswordReset([FromBody] PasswordResetAuthServiceDto dto)
        {
            Response response = await _authService.EmailPasswordResetAsnyc(dto.Email);
            return this.HandleResponse(response);
        }

        [HttpPost("email-verify-reset-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmailVerifyResetToken([FromBody] VerifyResetTokenAuthServiceDto dto)
        {
            Response response = await _authService.EmailVerifyResetTokenAsync(dto.ResetToken, dto.UserId);
            return this.HandleResponse(response);
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailAuthServiceDto dto)
        {
            Response response = await _authService.ConfirmEmailAsync(dto.UserId, dto.Token);
            return this.HandleResponse(response);
        }

        #region AddPhoneNumber
        [HttpPost("add-phone-number")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddPhoneNumber([FromBody] NumberVerifyRequest request)
        {
            Response response = await _authService.AddPhoneNumberAsync(request.Number, User.Identity.Name);
            return this.HandleResponse(response);
        }

        [HttpPost("verify-phone-number")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> VerifyPhoneNumber([FromBody] VerifyOtpRequest request)
        {
            Response response = await _authService.VerifyPhoneNumberAsync(User.Identity.Name, request.Otp);
            return this.HandleResponse(response);
        }
        #endregion

        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout([FromBody] LogoutAuthServiceDto dto)
        {
            Response response = await _authService.LogoutAsync(User.Identity.Name, dto.RefreshToken);
            return this.HandleResponse(response);
        }
        [HttpGet("check-number-verified")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckNumberVerified()
        {
            Response response = await _authService.CheckNumberVerifiedAsync(User.Identity.Name);
            return this.HandleResponse(response);
        }

        [HttpGet("check-email-verify")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckEmailVerify()
        {
            Response response = await _authService.CheckEmailVerifyAsync(User.Identity.Name);
            return this.HandleResponse(response);
        }

        [HttpPost("login-send-otp-to-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginSendOtpToEmailAsync([FromBody] LoginSendOtpToEmailAuthServiceDto dto)
        {
            Response response = await _authService.LoginSendOtpToEmailAsync(dto.Email);
            return this.HandleResponse(response);
        }

        [HttpPost("login-verify-otp-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginVerifyOtpEmailAsync([FromBody] LoginVerifyOtpEmailAuthServiceDto dto)
        {
            Response<Token> response = await _authService.LoginVerifyOtpEmailAsync(dto.Email, dto.Otp);
            return this.HandleResponse(response);
        }

        [HttpPost("login-send-otp-to-number")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginSendOtpToNumberAsync([FromBody] LoginSendOtpToNumberAuthServiceDto dto)
        {
            Response response = await _authService.LoginSendOtpToNumberAsync(dto.Number);
            return this.HandleResponse(response);
        }

        [HttpPost("login-verify-otp-number")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<Token>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginVerifyOtpNumberAsync([FromBody] LoginVerifyOtpNumberAuthServiceDto dto)
        {
            Response<Token> response = await _authService.LoginVerifyOtpNumberAsync(dto.Number, dto.Otp);
            return this.HandleResponse(response);
        }

        [HttpPost("super-admin-verify-set-password-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SuperAdminVerifySetPasswordToken([FromBody] SuperAdminVerifySetPasswordTokenAuthServiceDto dto)
        {
            Response response = await _authService.SuperAdminVerifySetPasswordTokenAsync(dto.Email, dto.Token);
            return this.HandleResponse(response);
        }

        [HttpPost("super-admin-set-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SuperAdminSetPassword([FromBody] SuperAdminSetPasswordAuthServiceDto dto)
        {
            Response response = await _authService.SuperAdminSetPasswordAsync(dto.Email, dto.Token, dto.Password);
            return this.HandleResponse(response);
        }
    }
}