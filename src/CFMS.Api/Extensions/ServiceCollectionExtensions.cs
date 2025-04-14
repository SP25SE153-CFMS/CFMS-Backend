using CFMS.Application.Behaviors;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Events;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Mappings;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure;
using CFMS.Infrastructure.Persistence;
using CFMS.Infrastructure.Repositories;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
            services.AddDbContext<CfmsDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Http
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            //Mappers
            services.AddAutoMapper(typeof(FarmProfile));

            //MediatR
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(CreateFarmCommandHandler).Assembly);
            });

            //Cache
            services.AddDistributedMemoryCache();

            //Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventQueueBehavior<,>));
            services.AddSingleton<EventQueue>();

            //Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IGoogleDriveService, GoogleDriveService>();
            services.AddScoped<DriveService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var credentialsFilePath = configuration["GoogleDrive:CredentialsFilePath"];

                if (string.IsNullOrEmpty(credentialsFilePath) || !File.Exists(credentialsFilePath))
                {
                    throw new FileNotFoundException("Không tìm thấy credentials.json", credentialsFilePath);
                }

                UserCredential credential;
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { DriveService.Scope.DriveFile },
                        "user", CancellationToken.None, new FileDataStore("GoogleDriveTokenStore", true)).Result;
                }

                return new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "GoogleDriveUploadApp"
                });
            });


            return services;
        }
    }
}
