using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System.Data.SqlClient;
using System.Linq;
using Tenders.AdsSearch;
using Tenders.Startup;

namespace Tenders
{
    public static class Program
    {
        public static void Main()
        {
            //IHostBuilder builder = Builder();
            //IHost host = builder.Build();
            //host.Run();

            Builder().Build().Run();
        }

        public static IHostBuilder Builder()
        {
            return Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<DatabaseMigrationsHostedService>();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<QuartzJobFactory>();
                    services.AddSingleton(new QuartzJobRegistry());
                    services.AddTransient<QuartzJobExecutor>();
                    //TODO: uncomment later
                    //services.AddHostedService<QuartzSchedulerHostedService>();
                })
                .ConfigureServices(services =>
                {
                    services.AddTransient<SqlConnectionFactory>();
                    services.AddSingleton(new SqlConnectionStringBuilder
                    {
                        DataSource = "localhost",
                        InitialCatalog = "Bzp",
                        IntegratedSecurity = true,
                    });
                })
                .ConfigureServices(services =>
                {
                    services.Repeat<AdsSearch.AdsSearchJob>();
                });
        }

        private static IServiceCollection Repeat<T>(this IServiceCollection services) where T : class, IJob
        {
            services.AddTransient<T>();
            services.Get<QuartzJobRegistry>().Repeat<T>();
            return services;
        }

        public static T Get<T>(this IServiceCollection services)
        {
            return (T)services.FirstOrDefault(s => s.ImplementationInstance?.GetType() == typeof(T)).ImplementationInstance;
        }
    }
}
