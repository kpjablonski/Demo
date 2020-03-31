using System;
using System.Data.SqlClient;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=master;Trusted_Connection=True;";
            connection.Open();

            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Create Database Bzp";
            command.ExecuteNonQuery();

            connection.Close();
            connection.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";

            var createTableAdsCommand = new SqlCommand();
            createTableAdsCommand.Connection = connection;
            createTableAdsCommand.CommandText = 
                @"Create Table Ads (
                Id BIGINT PRIMARY KEY,
                Number TEXT 
                )
                INSERT INTO Ads (Id, Number)
                VALUES (3 , 'x')";

            connection.Open();
            createTableAdsCommand.ExecuteNonQuery();
            connection.Close();

            //var insertNewValues = new SqlCommand();
            //insertNewValues.Connection = connection;



        }
    }
}
