using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public class AdsSearchJob : IJob
    {
        private readonly SqlConnectionFactory connectionFactory;
        private readonly IBzpWebsite website;

        public AdsSearchJob(
            IBzpWebsite website,
            SqlConnectionFactory connectionFactory)
        {
            this.website = website;
            this.connectionFactory = connectionFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            DateTime searchExecutionDate = DateTime.Now.Date;
            await using SqlConnection connection = await connectionFactory.CreateAsync();
            
            AdsSearchCriteria searchCriteria = await connection.GetAdsSearchCriteriaAsync();

            // 1. tu będziemy szukac ogłoszeń w bzp
            AdsSearchResult searchResult = await website.SearchAsync(searchCriteria);

            // 2. pobierzemy ogłoszenia z bazy ktore mamy
            List<AdSearchAd> actual = await connection.ListAdSearchAdsAsync(searchCriteria.PublicationDate); // todo: optymalizacja - pobrać tylko te ogłoszenia, których data publikacji jest taka sama jak data z searchCriteria
            List<string> actualNumbers = actual.Select(s => s.Number).ToList();

            // 3. nowe ogłoszenia
            var newads = new List<AdSearchAd>();
            foreach (var ad in searchResult.Ads)
            {
                if (!actualNumbers.Contains(ad.Number))
                {
                    newads.Add(ad);
                }
            }

            // 4. dodamy nowe ogłoszenia do bazy
            await connection.SaveAsync(newads);
            
            //searchResult.Ads

            AdsSearchCriteria nextSearchCriteria = searchCriteria.Next(searchExecutionDate);
            await connection.SaveAsync(nextSearchCriteria);
        }


    }
}
