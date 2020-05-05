using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tenders.Startup;
using System.Collections.Generic;

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
                    .AddHostedService<DropDatabaseHostedService>()
                    .AddTransient<IBzpWebsite, BzpWebsiteMock>())
                .Build();

            var bzp = App.Services.GetRequiredService<SqlConnectionStringBuilder>();
            bzp.InitialCatalog += Guid.NewGuid().ToString("N");
            bzp.Pooling = false;
        }

        [TestMethod]
        public async Task AdsSearchCriteriaChangesProperly1()
        {
            await App.ExecuteJobAsync<AdsSearchJob>();

            using var connection = await App.Services.GetRequiredService<SqlConnectionFactory>().CreateAsync();

            AdsSearchCriteria searchCriteria = await connection.GetAdsSearchCriteriaAsync();

            Assert.AreEqual(new DateTime(2017, 05, 02), searchCriteria.PublicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task AdsSearchCriteriaChangesProperly2()
        {
            await App.ExecuteJobAsync<AdsSearchJob>();
            await App.ExecuteJobAsync<AdsSearchJob>();
            await App.ExecuteJobAsync<AdsSearchJob>();

            using SqlConnection connection = await App.Services.GetRequiredService<SqlConnectionFactory>().CreateAsync();
            AdsSearchCriteria searchCriteria =  await connection.GetAdsSearchCriteriaAsync();

            Assert.AreEqual(new DateTime(2017, 05, 04), searchCriteria.PublicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task AdsSearchCriteriaChangesProperly3()
        {
            using var connection = await App.Services.GetRequiredService<SqlConnectionFactory>().CreateAsync();

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

        [TestMethod]
        public async Task PersistingAds1()
        {
            var connectionFactory = App.Services.GetRequiredService<SqlConnectionFactory>();
            await using SqlConnection connection = await connectionFactory.CreateAsync();

            await App.ExecuteJobAsync<AdsSearchJob>();

            List<AdSearchAd> actual = await connection.ListAdSearchAdsAsync();
            Assert.AreEqual(3, actual.Count);
            
            AdSearchAd element1 = actual[0];
            Assert.AreEqual("Number1", element1.Number);
            Assert.AreEqual("Url1", element1.Url);
            Assert.AreEqual("PublicationDate1", element1.PublicationDate);
        }

        [TestMethod]
        public async Task PersistingAds2()
        {
            var connectionFactory = App.Services.GetRequiredService<SqlConnectionFactory>();
            await using SqlConnection connection = await connectionFactory.CreateAsync();

            await App.ExecuteJobAsync<AdsSearchJob>();
            await App.ExecuteJobAsync<AdsSearchJob>();

            List<AdSearchAd> actual = await connection.ListAdSearchAdsAsync();
            Assert.AreEqual(3, actual.Count);
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
