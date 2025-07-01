using Application.Abstractions.Services;
using Application.Enums;
using Application.Helpers;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration, IMailService mailService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleExist = await roleManager.RoleExistsAsync(configuration.GetValue<string>("SuperAdmin:Role"));
            if (!roleExist)
            {
                var role = new AppRole { Name = configuration.GetValue<string>("SuperAdmin:Role"), Id = Guid.NewGuid().ToString() };
                await roleManager.CreateAsync(role);
            }

            var managerRoleExist = await roleManager.RoleExistsAsync(Roles.Manager.ToString());
            if (!managerRoleExist)
            {
                var role = new AppRole { Name = Roles.Manager.ToString(), Id = Guid.NewGuid().ToString() };
                await roleManager.CreateAsync(role);
            }

            var userRoleExist = await roleManager.RoleExistsAsync(Roles.Customer.ToString());
            if (!userRoleExist)
            {
                var role = new AppRole { Name = Roles.Customer.ToString(), Id = Guid.NewGuid().ToString() };
                await roleManager.CreateAsync(role);
            }

            var superAdminUser = await userManager.FindByNameAsync(configuration.GetValue<string>("SuperAdmin:Mail"));
            if (superAdminUser == null)
            {
                superAdminUser = new AppUser { UserName = configuration.GetValue<string>("SuperAdmin:UserName"), Email = configuration.GetValue<string>("SuperAdmin:Mail"), Id = Guid.NewGuid().ToString(), EmailConfirmed = true, Name = configuration.GetValue<string>("SuperAdmin:UserName"), Surname = configuration.GetValue<string>("SuperAdmin:UserName") };
                var createUserResult = await userManager.CreateAsync(superAdminUser);
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdminUser, configuration.GetValue<string>("SuperAdmin:Role"));

                    var token = await userManager.GeneratePasswordResetTokenAsync(superAdminUser);
                    token = token.UrlEncode();
                    var resetLink = $"{configuration.GetValue<string>("FrontClientUrl")}/super-admin-set-password/{configuration.GetValue<string>("SuperAdmin:Mail")}/{token}";
                    await mailService.SendMailAsync(superAdminUser.Email, "Super Admin Reset Password", $"Please reset your password using this link: {resetLink}");
                }
            }
        }
    }
}
