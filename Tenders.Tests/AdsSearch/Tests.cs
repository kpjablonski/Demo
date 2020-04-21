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
        public void MyTestMethod()
        {

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
