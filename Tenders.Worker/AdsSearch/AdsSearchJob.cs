using Quartz;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public class AdsSearchJob : IJob
    {
        private readonly SqlConnectionFactory connectionFactory;

        public AdsSearchJob(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            DateTime searchExecutionDate = DateTime.Now.Date;

            using (SqlConnection connection = await connectionFactory.Create())
            {
                AdsSearchCriteria searchCriteria = await connection.GetAdsSearchCriteriaAsync();

                await connection.SaveAsync(searchCriteria.Next(searchExecutionDate));
            }
        }


    }
}
