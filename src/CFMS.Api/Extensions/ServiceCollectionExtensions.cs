using CFMS.Application.Behaviors;
using CFMS.Application.Events;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Mappings;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Persistence;
using CFMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Mapper
            services.AddAutoMapper(typeof(FarmProfile));

            //DbContext
            services.AddDbContext<CfmsDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

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

            //Handlers
            //services.AddScoped<IRequestHandler<SignInCommand, BaseResponse<AuthResponse>>, SignInCommandHandler>();
            //services.AddScoped<IRequestHandler<SignUpCommand, BaseResponse<AuthResponse>>, SignUpCommandHandler>();

            return services;
        }
    }
}
