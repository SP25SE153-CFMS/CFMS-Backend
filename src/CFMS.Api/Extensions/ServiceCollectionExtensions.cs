using CFMS.Application.Behaviors;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Events;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Mappings;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Persistence;
using CFMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CFMS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                var appSettingsJson = Environment.GetEnvironmentVariable("APPSETTINGS_JSON");

                if (!string.IsNullOrEmpty(appSettingsJson))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(appSettingsJson);
                        connectionString = doc.RootElement
                                              .GetProperty("ConnectionStrings")
                                              .GetProperty("DefaultConnection")
                                              .GetString();
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Failed to parse APPSETTINGS_JSON", ex);
                    }
                }
            }

            services.AddDbContext<CfmsDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

            //HttpContext
            services.AddHttpContextAccessor();

            //Mappers
            services.AddAutoMapper(typeof(FarmProfile));

            //MediatR
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(CreateFarmCommandHandler).Assembly);
            });

            //Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventQueueBehavior<,>));
            services.AddSingleton<EventQueue>();

            //Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
