using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Tenders.Startup
{
    public class QuartzSchedulerHostedService : IHostedService
    {
        IScheduler scheduler;
        private readonly QuartzJobFactory jobFactory;
        private readonly QuartzJobRegistry jobRegistry;

        public QuartzSchedulerHostedService(
            QuartzJobFactory jobFactory,
            QuartzJobRegistry jobRegistry)
        {
            this.jobFactory = jobFactory;
            this.jobRegistry = jobRegistry;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };

            var factory = new StdSchedulerFactory(props);
            scheduler = await factory.GetScheduler();
            scheduler.JobFactory = jobFactory;
            foreach (var schedule in jobRegistry)
            {
                await scheduler.ScheduleJob(
                    JobBuilder.Create(schedule.Key).Build(),
                    TriggerBuilder.Create().StartNow().WithSimpleSchedule(schedule.Value).Build());
            }

            await scheduler.Start();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await scheduler.Shutdown();
        }
    }
}
