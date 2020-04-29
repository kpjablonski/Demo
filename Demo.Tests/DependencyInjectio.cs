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
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IElixirClient, ElixirClient>();
            serviceCollection.AddTransient<Service1>();

            //serviceCollection.AddTransient<IElixirClient, ElixirClientMock>();

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            //Service1 service1 = new Service1(new ElixirClientMock());
            
            // prod:
            //Service1 service2 = new Service1(new ElixirClientMock());
            
            
            Service1 service1 = serviceProvider
                .GetRequiredService<Service1>();


        }
    }

    public class Service1
    {
        private readonly IElixirClient elixir;
        
        public Service1(IElixirClient elixir)
        {
            this.elixir = elixir;
        }

        public void Do()
        {
            elixir.PrzelejKase(1, "1231234");
        }
    }

    public interface IElixirClient
    {
        void PrzelejKase(decimal money, string account);
    }
    public class ElixirClient : IElixirClient
    {
        public void PrzelejKase(decimal money, string account)
        {
        }
    }
    public class ElixirClientMock : IElixirClient
    {
        public void PrzelejKase(decimal money, string account)
        {
            throw new NotImplementedException();
        }
    }
}
