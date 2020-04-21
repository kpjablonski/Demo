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
            await App.ExecuteAsync<AdsSearchJob>();

            using var connection = new SqlConnection();
            connection.ConnectionString = App.Services.GetRequiredService<SqlConnectionStringBuilder>().ConnectionString;
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PublicationDate FROM AdsSearchCriteria";
            object scalar = await command.ExecuteScalarAsync();
            var publicationDate = (DateTime)scalar;

            Assert.AreEqual(new DateTime(2017, 05, 02), publicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task MyTestMethod2()
        {
            await App.ExecuteAsync<AdsSearchJob>();
            await App.ExecuteAsync<AdsSearchJob>();

            using var connection = new SqlConnection();
            connection.ConnectionString = App.Services.GetRequiredService<SqlConnectionStringBuilder>().ConnectionString;
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PublicationDate FROM AdsSearchCriteria";
            object scalar = await command.ExecuteScalarAsync();
            var publicationDate = (DateTime)scalar;

            Assert.AreEqual(new DateTime(2017, 05, 03), publicationDate);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task MyTestMethod4()
        {
            await App.ExecuteAsync<AdsSearchJob>();
            await App.ExecuteAsync<AdsSearchJob>();
            await App.ExecuteAsync<AdsSearchJob>();

            using var connection = new SqlConnection();
            connection.ConnectionString = App.Services.GetRequiredService<SqlConnectionStringBuilder>().ConnectionString;
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PublicationDate FROM AdsSearchCriteria";
            object scalar = await command.ExecuteScalarAsync();
            var searchCriteriaPublicationDate = (DateTime)scalar;

            Assert.AreEqual(new DateTime(2017, 05, 04), searchCriteriaPublicationDate);

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
