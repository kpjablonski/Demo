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
