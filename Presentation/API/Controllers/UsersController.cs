using Api.Extensions;
using Application.Abstractions.Services;
using Application.DTOs.UserServiceDTOs;
using Application.DTOs.UserServiceDTOs.Responses;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.DTOs.UserServiceDTOs;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("update-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordUserServiceDto dto)
        {
            if (!dto.Password.Equals(dto.PasswordConfirm))
            {
                return BadRequest(new Response(ResponseStatusCode.ValidationError, "Password and confirmation do not match."));
            }

            Response response = await _userService.UpdatePasswordAsync(dto.UserId, dto.ResetToken, dto.Password);
            return this.HandleResponse(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(CreateUserUserServiceDTOs dto)
        {
            var response = await _userService.CreateAsync(new()
            {
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                Username = dto.Username,
                Password = dto.Password,
                PasswordConfirm = dto.PasswordConfirm,
                FinCode = dto.FinCode,
                PhoneNumber = dto.PhoneNumber
            });

            return this.HandleResponse(response);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetAllUsersUserSeviceResponseDTOs>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<GetAllUsersUserSeviceResponseDTOs>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<GetAllUsersUserSeviceResponseDTOs>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersUserSeviceDTOs dto)
        {
            var response = await _userService.GetAllUsersAsync(dto.Page, dto.Size);
            return this.HandleResponse(response);
        }

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllUsers()
        {
            Response response = await _userService.DeleteAllUsersAsync();
            return this.HandleResponse(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string[]>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string[]>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserUserService dto)
        {
            Response<string[]> response = await _userService.GetRolesToUserAsync(dto.UserId);
            return this.HandleResponse(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserUserService dto)
        {
            Response response = await _userService.AssignRoleToUserAsnyc(dto.UserId, dto.Roles);
            return this.HandleResponse(response);
        }
    }
}
