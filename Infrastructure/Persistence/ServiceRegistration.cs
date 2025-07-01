
﻿using Application.Repositories;
using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.Repositories.ReservationRepository;
using Application.MapperProfile;
using Persistence.Repositories.ReservationRepository;
using Application.Abstractions.Services;
using Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;


namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            //ReservationRepo
            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();


            //Services
            services.AddScoped<IReservationService, ReservationService>();


            //AutoMapper
            services.AddAutoMapper(typeof(ReservationProfile));

        
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Deploy")));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
        }
    }
}

