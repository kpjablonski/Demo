using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Demo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {
            var program = new Program();
            program.CreateDataBaseAndTables();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = ConnectionStrings.Master();
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
        public void InsertTest1()
        {
            // act
            Ogloszenie expected = new Ogloszenie();
            expected.Number = "4445566";
            expected.DataPublikacji = "30032020";
            expected.Miejscowosc = "Wilkasy";
            expected.Plik = "taki";
            expected.InsertAutomat();

            // assert

            List<Ogloszenie> ogloszenia = GetAllAds();

            Assert.AreEqual(1, ogloszenia.Count);
            Ogloszenie actual = ogloszenia[0];
            Assert.AreEqual(expected.Number, actual.Number);
            Assert.AreEqual(expected.DataPublikacji, actual.DataPublikacji);

        }

        private static List<Ogloszenie> GetAllAds()
        {
            var ogloszenia = new List<Ogloszenie>();

            var connection = new SqlConnection();
            connection.ConnectionString = ConnectionStrings.Bzp();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Ads";

            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                long id = (long)reader.GetValue(0);
                string number = (string)reader.GetValue(1);
                string dataPublikacji = (string)reader.GetValue(2);

                var wczytaneOgloszenie = new Ogloszenie()
                {
                    Id = id,
                    Number = number,
                    DataPublikacji = dataPublikacji
                };
                ogloszenia.Add(wczytaneOgloszenie);
                // odczyt wiersza
            }
            connection.Close();
            return ogloszenia;
        }

        [TestMethod]
        public void ShouldBePossibleToInsertTwoDifferentAds()
        {
            // act 

            Ogloszenie expected1 = new Ogloszenie();
            expected1.Number = "234";
            expected1.DataPublikacji = "456780011";
            expected1.Miejscowosc = "Zabki";
            expected1.Plik = "ptaki";

            Ogloszenie expected2 = new Ogloszenie();
            expected2.Number = "3333344";
            expected2.DataPublikacji = "11111";
            expected2.Miejscowosc = "Wuhan";
            expected2.Plik = "Covid19";

            expected1.InsertAutomat();
            expected2.InsertAutomat();
            
            List<Ogloszenie> ogloszenia = GetAllAds();

            Assert.AreEqual(2, ogloszenia.Count);
            
            Ogloszenie actual1 = ogloszenia[0];
            Assert.AreEqual(expected1.Number, actual1.Number);
            Assert.AreEqual(expected1.DataPublikacji, actual1.DataPublikacji);

            Ogloszenie actual2 = ogloszenia[1];
            Assert.AreEqual(expected2.Number, actual2.Number);
            Assert.AreEqual(expected2.DataPublikacji, actual2.DataPublikacji);
        }

        [TestMethod]
        public void ShouldNotBePossibleToInsertTheSameAdTwice()
        {
            // act 
            Ogloszenie ogl = new Ogloszenie();
            ogl.Number = "4445566";
            ogl.DataPublikacji = "30032020";
            ogl.Miejscowosc = "Wilkasy";
            ogl.Plik = "taki";

            ogl.InsertAutomat();

            Assert.ThrowsException<Exception>(ogl.InsertAutomat);
        }
    }
}
