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
            // stw�rz baze
            var program = new Program();
            program.CreateDataBaseAndTables();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // wywal baze
            var connection = new SqlConnection();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=master;Trusted_Connection=True;";
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
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";
        }

        [TestMethod]
        public void TestMethod1()
        {
            Ogloszenie ogl = new Ogloszenie();
            //ogl.Id = 1234;
            ogl.Number = 4445566;
            ogl.DataPublikacji = 30032020;
            ogl.Miejscowosc = "Wilkasy";
            ogl.Plik = "taki";

            ogl.InsertAutomat();
        }
        // dopisac metod� testow�  - [TestMethod] musi by� nad nazw� metody,
        // w kt�rej dodamy do bazy dwa r�ne og�oszenia

        [TestMethod]
        public void TestMethod2()
        {
            Ogloszenie ogl = new Ogloszenie();
            //ogl.Id = 1234;
            ogl.Number = 234;
            ogl.DataPublikacji = 456780011;
            ogl.Miejscowosc = "Zabki";
            ogl.Plik = "ptaki";

            ogl.InsertAutomat();
        }

        [TestMethod]
        public void TestMethod3()
        {
            Ogloszenie ogl = new Ogloszenie();
            //ogl.Id = 1234;
            ogl.Number = 3333344;
            ogl.DataPublikacji = 11111;
            ogl.Miejscowosc = "Wuhan";
            ogl.Plik = "Covid19";

            ogl.InsertAutomat();
        }
            

        
    }
}
