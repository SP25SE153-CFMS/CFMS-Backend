using Microsoft.Extensions.DependencyInjection;

namespace CFMS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return services;
        }
    }
}
