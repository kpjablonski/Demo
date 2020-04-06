using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Demo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            // stwórz baze
        }

        [TestCleanup]
        public void Cleanup()
        {
            // wywal baze
        }

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine($"Hello Grooby");
        }
    }
}
