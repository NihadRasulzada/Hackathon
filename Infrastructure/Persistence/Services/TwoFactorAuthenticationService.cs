using Application.Abstractions.Services;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
    {
        readonly UserManager<AppUser> _userManager;

        public TwoFactorAuthenticationService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> DisableTwoFactorAuthenticationAsync(string userName)
        {
            AppUser? user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }
            if (!user.TwoFactorEnabled)
            {
                return new Response(ResponseStatusCode.ValidationError, "Two-factor authentication is already disabled.");
            }
            IdentityResult result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if(!result.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder("Failed to disable two-factor authentication: ");
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                return new Response(ResponseStatusCode.ValidationError, errorMessage.ToString());
            }
            return new Response(ResponseStatusCode.Success, "Two-factor authentication disabled successfully.");

        }

        public async Task<Response> EnableTwoFactorAuthenticationAsync(string userName)
        {
            AppUser? user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new Response(ResponseStatusCode.NotFound, "User not found.");
            }
            if (user.TwoFactorEnabled)
            {
                return new Response(ResponseStatusCode.ValidationError, "Two-factor authentication is already enabled.");
            }
            IdentityResult result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if(!result.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder("Failed to enable two-factor authentication: ");
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                return new Response(ResponseStatusCode.ValidationError, errorMessage.ToString());
            }
            return new Response(ResponseStatusCode.Success, "Two-factor authentication enabled successfully.");
        }
    }
}
