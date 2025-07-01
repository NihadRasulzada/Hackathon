using Application.Repositories;
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

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Sql
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Deploy")));

            //Rate Limiter
            /*services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();*/


            //FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));


            //Repository
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            //ReservationRepo
            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();


            //Services
            services.AddScoped<IReservationService, ReservationService>();


            //AutoMapper
            services.AddAutoMapper(typeof(ReservationProfile));

        }
    }

}