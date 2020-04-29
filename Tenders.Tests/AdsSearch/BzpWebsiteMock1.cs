using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    internal class BzpWebsiteMock : IBzpWebsite
    {
        public async Task<AdsSearchResult> SearchAsync(AdsSearchCriteria searchCriteria)
        {
            await Task.CompletedTask;
            return new AdsSearchResult
            {
                Ads = new System.Collections.Generic.List<AdSearchAd>
                {
                    new AdSearchAd
                    {
                        Number = "Number1",
                        Url = "Url1",
                    },
                    new AdSearchAd
                    {
                        Number = "Number2",
                        Url = "Url2"
                    },
                    new AdSearchAd
                    {
                        Number = "Number3",
                        Url = "Url3"
                    }
                }
            };
        }
    }
}