using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Tenders.Startup
{
    class DropDatabaseHostedService : IHostedService
    {
        private readonly SqlConnectionStringBuilder bzp;
        public DropDatabaseHostedService(SqlConnectionStringBuilder bzp)
        {
            this.bzp = bzp;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var master = new SqlConnectionStringBuilder
            {
                ConnectionString = bzp.ConnectionString,
                InitialCatalog = "master"
            };
            
            using var connection = new SqlConnection();
            connection.ConnectionString = master.ConnectionString;

            using var command = connection.CreateCommand();
            command.CommandText = $"DROP DATABASE {bzp.InitialCatalog}";

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
