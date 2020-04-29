using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public static class AdSearchAdRepository
    {
        public static async Task<List<AdSearchAd>> ListAdSearchAdsAsync(this SqlConnection connection)
        {
            var ads = new List<AdSearchAd>();

            await using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Ads";
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var ad = new AdSearchAd();
                ad.Number = (string)reader["Number"];
                ad.Url = (string)reader["Url"];
                ads.Add(ad);
            }

            return ads;
        }

        public static async Task SaveAsync(this SqlConnection connection, List<AdSearchAd> ads)
        {
            foreach (var ad in ads)
            {
                await using SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Ads (Number, Url) VALUES (@Number, @Url)";
                command.Parameters.Add(new SqlParameter("@Number", ad.Number));
                command.Parameters.Add(new SqlParameter("@Url", ad.Url));
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
