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
            var program = new Program();
            program.CreateDataBaseAndTables();

            Ogloszenie ogl = new Ogloszenie();
            //ogl.Id = 1234;
            ogl.Number = 4445566;
            ogl.DataPublikacji = 30032020;
            ogl.Miejscowosc = "Wilkasy";
            ogl.Plik = "taki";

            ogl.InsertAutomat();
        }

        // dopisac metodê testow¹  - [TestMethod] musi byæ nad nazw¹ metody,
        // w której dodamy do bazy dwa ró¿ne og³oszenia
    }
}
