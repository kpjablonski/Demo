using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Tenders.Startup
{
    public class DatabaseMigrationsHostedService : IHostedService
    {
        private readonly SqlConnectionStringBuilder bzp;

        public DatabaseMigrationsHostedService(
            SqlConnectionStringBuilder bzp)
        {
            this.bzp = bzp;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await CreateDatabaseAsync();
            await CreateAdsSearchCriteriaTableAsync();
            await CreateAdsTableAsync();
        }

        private async Task CreateAdsTableAsync()
        {
            using var connection = new SqlConnection();
            connection.ConnectionString = bzp.ConnectionString;
            await connection.OpenAsync();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
            IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE name = 'Ads' AND xtype = 'U')
            BEGIN
                CREATE TABLE Ads (Number VARCHAR(255), Url TEXT, PublicationDate Date);
                CREATE UNIQUE INDEX IND_Number_Unique ON dbo.Ads (Number);
            END;";
            await command.ExecuteNonQueryAsync();
        }

        private async Task CreateAdsSearchCriteriaTableAsync()
        {
            using var connection = new SqlConnection();
            connection.ConnectionString = bzp.ConnectionString;
            await connection.OpenAsync();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
            IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE name = 'AdsSearchCriteria' AND xtype = 'U')
            BEGIN
                CREATE TABLE AdsSearchCriteria (PublicationDate Date);
                INSERT INTO AdsSearchCriteria (PublicationDate) VALUES ('2017-05-01');
            END;";
            await command.ExecuteNonQueryAsync();
        }

        private async Task CreateDatabaseAsync()
        {
            var master = new SqlConnectionStringBuilder()
            {
                ConnectionString = bzp.ConnectionString,
                InitialCatalog = "master"
            };

            using var connection = new SqlConnection();
            connection.ConnectionString = master.ConnectionString;
            
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = $@"
            IF NOT EXISTS(SELECT 1 FROM sysdatabases WHERE name = '{bzp.InitialCatalog}')
            BEGIN
            CREATE DATABASE {bzp.InitialCatalog};
            END";
            
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }        
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
