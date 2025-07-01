using Api.Extensions;
using Application.Abstractions.Services;
using Application.ResponceObject;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;
        public TwoFactorAuthenticationController(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }
        [HttpPost("enable-two-factor-authentication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            Response response = await _twoFactorAuthenticationService.EnableTwoFactorAuthenticationAsync(User.Identity.Name);
            return this.HandleResponse(response);
        }

        [HttpPost("disable-two-factor-authentication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            Response response = await _twoFactorAuthenticationService.DisableTwoFactorAuthenticationAsync(User.Identity.Name);
            return this.HandleResponse(response);
        }
    }
}
