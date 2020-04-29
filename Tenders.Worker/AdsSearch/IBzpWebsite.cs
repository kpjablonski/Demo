using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public interface IBzpWebsite
    {
        Task<AdsSearchResult> SearchAsync(AdsSearchCriteria searchCriteria);
    }

    public class BzpWebsite : IBzpWebsite
    {
        public Task<AdsSearchResult> SearchAsync(AdsSearchCriteria searchCriteria)
        {
            throw new NotImplementedException("To zrobi gruby!");
        }
    }
}
