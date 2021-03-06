﻿using System;
using System.Data.SqlClient;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }

        public void CreateDataBaseAndTables()
        {
            // database
            var connection = new SqlConnection();
            connection.ConnectionString = ConnectionStrings.Master();
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
            connection.ConnectionString = ConnectionStrings.Bzp();

            // table

            var createTableAdsCommand = new SqlCommand();
            createTableAdsCommand.Connection = connection;
            createTableAdsCommand.CommandText =
               @"IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE name = 'Ads' AND xtype = 'U')
                BEGIN
                    CREATE TABLE Ads(
                        Id BIGINT IDENTITY(1, 1),
                        Number TEXT,
                        DataPublikacji TEXT,
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
