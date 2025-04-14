using CFMS.Application.Services.Quarzt;
using Quartz;

namespace CFMS.Api.Extensions
{
    public static class QuartzExtensions
    {
        public static IServiceCollection AddQuartzServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("UpdateChickenbatchCurrentStageJob");

                q.AddJob<UpdateChickenbatchCurrentStageJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateChickenbatchCurrentStageJob-trigger")
                    .WithCronSchedule("0 0 0 * * ?")
                );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
