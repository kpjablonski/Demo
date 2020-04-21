using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Tests
{
    [TestClass]
    public class ListDemo
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var list = new List<Ad>();
            list.Add(new Ad { Number = "Wacek" });
            list.Add(new Ad { Number = "Kota" });

            List<string> numberList = list.Select(s => s.Number).ToList();
        }

        [TestMethod]
        public void MyTestMethod2()
        {
            Assert.AreEqual("Kota", "Kota");
            var someSpecifiedAddressInmemory = new Ad { Number = "Kota" };
            var first = someSpecifiedAddressInmemory;
            var second = someSpecifiedAddressInmemory;
            
            Assert.AreEqual(first, second);
        }
    }

    public class Ad
    {

        public string Number { get; set; }
    }
}
