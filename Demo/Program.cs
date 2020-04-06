using System;
using System.Data.SqlClient;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDataBaseAndTables();

            // insert
            // inser mozna zastapic ad i ads klasami ktore byly tworzone
            var connection2 = new SqlConnection();
            connection2.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = @"
                INSERT INTO Ads (Id, Number, DataPublikacji, Miejscowosc, Plik)
                VALUES (3 , '510059865-N-2020', '06_04_2020', 'Wilkasy', 'Zobacz' )";

            insertData.ExecuteNonQuery();
            connection2.Close();

            //var insertNewValues = new SqlCommand();
            //insertNewValues.Connection = connection;

            //BLEDY:
            //String or binary data would be truncated
            // Tabela byla nastepujaca:
            // CREATE TABLE Ads(
            //Id BIGINT PRIMARY KEY,
            //Number TEXT,
            //            Data_Publikacji VARCHAR(255),
            //            Miejscowosc TEXT,
            //            Plik VARCHAR(255),



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
