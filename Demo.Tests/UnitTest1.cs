using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace Demo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            var program = new Program();
            program.CreateDataBaseAndTables();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var connectionString = new ConnectionStrings();
            var connection = new SqlConnection();
            connection.ConnectionString = connectionString.Master();
            connection.Open();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"
             IF EXISTS(SELECT 1 FROM sysdatabases WHERE name = 'Bzp')
             BEGIN
                ALTER DATABASE Bzp
                SET SINGLE_USER
                WITH ROLLBACK IMMEDIATE;
                DROP DATABASE Bzp;
             END";
            command.ExecuteNonQuery();

            connection.Close();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Ogloszenie ogl = new Ogloszenie();
            ogl.Number = 4445566;
            ogl.DataPublikacji = 30032020;
            ogl.Miejscowosc = "Wilkasy";
            ogl.Plik = "taki";

            ogl.InsertAutomat();
        }

        [TestMethod]
        public void TestMethod2()
        {
            Ogloszenie ogl1 = new Ogloszenie();
            ogl1.Number = 234;
            ogl1.DataPublikacji = 456780011;
            ogl1.Miejscowosc = "Zabki";
            ogl1.Plik = "ptaki";

            ogl1.InsertAutomat();

            Ogloszenie ogl2 = new Ogloszenie();
            ogl2.Number = 3333344;
            ogl2.DataPublikacji = 11111;
            ogl2.Miejscowosc = "Wuhan";
            ogl2.Plik = "Covid19";

            ogl2.InsertAutomat();
        }
    }
}
