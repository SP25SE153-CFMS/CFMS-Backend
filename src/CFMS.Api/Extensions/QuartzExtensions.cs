using CFMS.Application.Services.Quartz;
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
                    .WithCronSchedule(configuration["Quartz:UpdateChickenbatchCurrentStageJob"] ?? "0 0 0 * * ?")
                );
            });
            
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("CheckStartDateChickenBatchJob");

                q.AddJob<CheckStartDateChickenBatchJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CheckStartDateChickenBatchJob-trigger")
                    .WithCronSchedule(configuration["Quartz:CheckStartDateChickenBatchJob"] ?? "0 0 0 * * ?")
                );
            });
            
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("CheckWareStockJob");

                q.AddJob<CheckWareStockJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CheckWareStockJob-trigger")
                    .WithCronSchedule(configuration["Quartz:CheckWareStockJob"] ?? "0 0 0 * * ?")
                );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
