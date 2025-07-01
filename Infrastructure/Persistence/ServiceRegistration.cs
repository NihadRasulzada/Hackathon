using Application.Abstractions.Services;
using Application.MapperProfile;
using Application.MappingProfiles;
using Application.Repositories.ReservationRepository;
using Application.Repositories.ServiceRepository;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.ReservationRepository;
using Persistence.Repositories.ServiceRepository;
using Persistence.Services;



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
            services.AddScoped<IReservationService, ReservationServices>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationServicesService, ReservationServicesService>();





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

