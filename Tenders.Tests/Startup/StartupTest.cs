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
            var builder = Program.Builder();
            var app = builder.Build();
            await app.StartAsync();
            await app.StopAsync();
        }
    }
}
