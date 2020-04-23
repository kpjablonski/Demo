using Quartz;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public class AdsSearchJob : IJob
    {
        private readonly SqlConnectionStringBuilder bzp;

        public AdsSearchJob(SqlConnectionStringBuilder bzp)
        {
            this.bzp = bzp;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            DateTime searchExecutionDate = DateTime.Now.Date;
            DateTime publicationDate = await GetSearchCriteria();
            DateTime nextPublicationDate = publicationDate.AddDays(1);
            if (nextPublicationDate > searchExecutionDate)
            {
                nextPublicationDate = nextPublicationDate.AddDays(-1);
            }
            using var connection = new SqlConnection();
            connection.ConnectionString = bzp.ConnectionString;
            await connection.OpenAsync();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE AdsSearchCriteria SET PublicationDate = @PublicationDate;";
            command.Parameters.Add(new SqlParameter("@PublicationDate", nextPublicationDate));
            await command.ExecuteNonQueryAsync();
        }

        public async Task<DateTime> GetSearchCriteria()
        {
            using var connection = new SqlConnection();
            connection.ConnectionString = bzp.ConnectionString;
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PublicationDate FROM AdsSearchCriteria";
            object scalar = await command.ExecuteScalarAsync();
            DateTime searchCriteriaPublicationDate = (DateTime)scalar;

            return searchCriteriaPublicationDate;
        }
    }
}
