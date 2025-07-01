using Application.Abstractions.Services;
using Application.MapperProfile;
using Application.Repositories.ReservationRepository;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.ReservationRepository;
using Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;
using Application.MappingProfiles;
using Application.Repositories.ServiceRepository;
using Persistence.Repositories.ServiceRepository;



namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //ReservationRepo
            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();

            //ServiceRepo
            services.AddScoped<IServiceReadRepository, ServiceReadRepository>();
            services.AddScoped<IServiceWriteRepository, ServiceWriteRepository>();



            //Services
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IServicesService, ServicesService>();



            //AutoMapper
            services.AddAutoMapper(typeof(ReservationProfile));
            services.AddAutoMapper(typeof(ServiceProfile));


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

