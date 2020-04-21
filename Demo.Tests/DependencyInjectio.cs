using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Tests
{
    [TestClass]
    class DependencyInjectio
    {
        [TestMethod]
        public void MyTestMethod()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<Service1>();
            IServiceProvider provider = services.BuildServiceProvider();
            Service1 service1 = provider.GetRequiredService<Service1>();

            
        }
    }

    public class Service1
    {
        private readonly ILogger logger;
        public Service1(ILogger logger)
        {
            this.logger = logger;
        }

        public void Do()
        {
            logger.LogInformation("Do completed");
        }
    }
}
