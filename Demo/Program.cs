using System;
using System.Data.SqlClient;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            NewMethod();

            // insert
            var connection2 = new SqlConnection();
            connection2.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText =
                @"
                INSERT INTO Ads (Id, Number)
                VALUES (3 , 'x')";

            insertData.ExecuteNonQuery();
            connection2.Close();

            //var insertNewValues = new SqlCommand();
            //insertNewValues.Connection = connection;



        }

        private static void NewMethod()
        {
            // database
            var connection = new SqlConnection();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=master;Trusted_Connection=True;";
            connection.Open();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Create Database Bzp";
            command.ExecuteNonQuery();

            connection.Close();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";

            // table

            var createTableAdsCommand = new SqlCommand();
            createTableAdsCommand.Connection = connection;
            createTableAdsCommand.CommandText =
                @"Create Table Ads (
                Id BIGINT PRIMARY KEY,
                Number TEXT 
                )";


            connection.Open();
            createTableAdsCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}
