using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Tenders.Startup
{
    [TestClass]
    public class StartupTest
    {
        [TestMethod]
        public async Task Test()
        {
            IHost app = Program.Builder().Build();
            await app.StartAsync();
            await app.StopAsync();
        }
    }
}
