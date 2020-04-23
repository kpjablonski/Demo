using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tenders.Startup;

namespace Tenders.AdsSearch
{
    [TestClass]
    public class Tests
    {
        private readonly IHost App;

        public Tests()
        {
            App = Program.Builder()
                .ConfigureServices(services => services
                    .AddHostedService<DropDatabaseHostedService>())
                .Build();

            var bzp = App.Services.GetRequiredService<SqlConnectionStringBuilder>();
            bzp.InitialCatalog += Guid.NewGuid().ToString("N");
            bzp.Pooling = false;
        }

        [TestMethod]
        public async Task MyTestMethod1()
        {
            await App.ExecuteJobAsync<AdsSearchJob>();

            using var connection = await App.Services.GetRequiredService<SqlConnectionFactory>().Create();

            AdsSearchCriteria searchCriteria = await connection.GetAdsSearchCriteriaAsync();

            Assert.AreEqual(new DateTime(2017, 05, 02), searchCriteria.PublicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task MyTestMethod2()
        {
            await App.ExecuteJobAsync<AdsSearchJob>();
            await App.ExecuteJobAsync<AdsSearchJob>();
            await App.ExecuteJobAsync<AdsSearchJob>();

            using var connection = await App.Services.GetRequiredService<SqlConnectionFactory>().Create();

            AdsSearchCriteria searchCriteria =  await connection.GetAdsSearchCriteriaAsync();

            Assert.AreEqual(new DateTime(2017, 05, 04), searchCriteria.PublicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task MyTestMethod3()
        {
            using var connection = await App.Services.GetRequiredService<SqlConnectionFactory>().Create();

            // arrange
            var searchCriteriaBeforeJobExecution = new AdsSearchCriteria
            {
                PublicationDate = DateTime.Now.Date
            };
            await connection.SaveAsync(searchCriteriaBeforeJobExecution);
            
            // act
            await App.ExecuteJobAsync<AdsSearchJob>();
            
            // assert
            AdsSearchCriteria searchCriteriaAfterJobExecution = await connection.GetAdsSearchCriteriaAsync();
            Assert.AreEqual(searchCriteriaBeforeJobExecution.PublicationDate, searchCriteriaAfterJobExecution.PublicationDate);
            await connection.CloseAsync();
        }


        [TestInitialize]
        public async Task Initialize()
        {
            await App.StartAsync();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await App.StopAsync();
        }
    }
}
