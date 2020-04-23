using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tenders.Startup;

namespace Tenders
{
    public static class Act
    {
        public static async Task ExecuteJobAsync<T>(this IHost host) where T : IJob
        {
            var executor = host.Services.GetRequiredService<QuartzJobExecutor>();
            await executor.Execute(new QuartzTestExecutionContext<T>());
        }
    }

    class QuartzTestExecutionContext<T> : IJobExecutionContext where T : IJob
    {
        public IScheduler Scheduler { get; }
        public ITrigger Trigger { get; }
        public ICalendar Calendar { get; }
        public bool Recovering { get; }
        public TriggerKey RecoveringTriggerKey { get; }
        public int RefireCount { get; }
        public JobDataMap MergedJobDataMap { get; }
        public IJobDetail JobDetail { get; } = new TestJobDetail();
        public IJob JobInstance { get; }
        public DateTimeOffset FireTimeUtc { get; }
        public DateTimeOffset? ScheduledFireTimeUtc { get; }
        public DateTimeOffset? PreviousFireTimeUtc { get; }
        public DateTimeOffset? NextFireTimeUtc { get; }
        public string FireInstanceId { get; }
        public object Result { get; set; }
        public TimeSpan JobRunTime { get; }
        public CancellationToken CancellationToken { get; }
        public object Get(object key)
        {
            throw new NotImplementedException();
        }
        public void Put(object key, object objectValue)
        {
            throw new NotImplementedException();
        }
        class TestJobDetail : IJobDetail
        {
            public JobKey Key { get; }
            public string Description { get; }
            public Type JobType { get; } = typeof(T);
            public JobDataMap JobDataMap { get; }
            public bool Durable { get; }
            public bool PersistJobDataAfterExecution { get; }
            public bool ConcurrentExecutionDisallowed { get; }
            public bool RequestsRecovery { get; }
            public IJobDetail Clone()
            {
                throw new NotImplementedException();
            }
            public JobBuilder GetJobBuilder()
            {
                throw new NotImplementedException();
            }
        }
    }
}
