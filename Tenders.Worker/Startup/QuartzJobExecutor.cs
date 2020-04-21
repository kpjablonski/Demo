using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace Tenders.Startup
{
    public class QuartzJobExecutor : IJob
    {
        private readonly IServiceProvider provider;        
        public QuartzJobExecutor(IServiceProvider provider)
        {
            this.provider = provider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = provider.CreateScope();
            try
            {
                var job = (IJob)scope.ServiceProvider.GetRequiredService(context.JobDetail.JobType);
                await job.Execute(context);
            }
            catch (Exception exception)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<QuartzJobExecutor>>();
                logger.LogError(exception, exception.Message);
                throw;
            }            
        }
    }
}
