using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace Tenders.Startup
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;
        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetRequiredService<QuartzJobExecutor>();
        }
        public void ReturnJob(IJob job)
        {
        }
    }
}
