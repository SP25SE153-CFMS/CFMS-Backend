using CFMS.Domain.Enums.Roles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CFMS.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            Convert.FromBase64String(configuration["Jwt:AccessSecretKey"])
                        )
                    };
                })
                .AddCookie()
                .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
                   {
                       options.ClientId = configuration["Authentication:Google:ClientId"];
                       options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                       options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                       options.CorrelationCookie.SameSite = SameSiteMode.None;
                   });

            AddAuthorizationPolicies(services);
            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, ((int)GeneralRole.ADMIN_ROLE).ToString()));
                options.AddPolicy("UserOrAdmin", policy => policy.RequireClaim(ClaimTypes.Role, ((int)GeneralRole.USER_ROLE).ToString(), ((int)GeneralRole.ADMIN_ROLE).ToString()));
            });

            return services;
        }
    }
}
