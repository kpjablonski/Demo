using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public static class AdsSearchCriteriaRepository
    {
        public static async Task<AdsSearchCriteria> GetAdsSearchCriteriaAsync(this SqlConnection connection)
        {
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT PublicationDate FROM AdsSearchCriteria";            
            var adsSearchCriteria = new AdsSearchCriteria();
            adsSearchCriteria.PublicationDate = (DateTime)await command.ExecuteScalarAsync();
            return adsSearchCriteria;
        }
        
        public static async Task SaveAsync(this SqlConnection connection, AdsSearchCriteria searchCriteria)
        {
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE AdsSearchCriteria SET PublicationDate = @PublicationDate;";
            command.Parameters.Add(new SqlParameter("@PublicationDate", searchCriteria.PublicationDate));
            await command.ExecuteNonQueryAsync();
        }
    }
}
