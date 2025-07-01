using Application.ResponceObject;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface ITwoFactorAuthenticationService
    {
        Task<Response> EnableTwoFactorAuthenticationAsync(string userName);
        Task<Response> DisableTwoFactorAuthenticationAsync(string userName);
    }
}
