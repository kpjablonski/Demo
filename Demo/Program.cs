using System;
using System.Data.SqlClient;

namespace Demo
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateDataBaseAndTables();

            Ogloszenie ogl = new Ogloszenie();
            ogl.Id = 1234;
            ogl.Number = 4445566;
            ogl.DataPublikacji = 30032020;
            ogl.Miejscowosc = "Wilkasy";
            ogl.Plik = "taki";


            ogl.InsertAutomat();
        }

        private static void CreateDataBaseAndTables()
        {
            // database
            var connection = new SqlConnection();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=master;Trusted_Connection=True;";
            connection.Open();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"
             IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = 'Bzp')
             BEGIN
                CREATE DATABASE Bzp;
             END";
            command.ExecuteNonQuery();

            connection.Close();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";

            // table

            var createTableAdsCommand = new SqlCommand();
            createTableAdsCommand.Connection = connection;
            createTableAdsCommand.CommandText =
               @"IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE name = 'Ads' AND xtype = 'U')
                BEGIN
                    CREATE TABLE Ads(
                        Id BIGINT PRIMARY KEY,
                        Number TEXT,
                        DataPublikacji VARCHAR(255),
                        Miejscowosc TEXT,
                        Plik TEXT,
                    )
                END";


            connection.Open();
            createTableAdsCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}
