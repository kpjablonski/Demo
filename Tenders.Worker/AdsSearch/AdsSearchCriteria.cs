using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public class AdsSearchCriteria
    {
        public DateTime PublicationDate { get; set; }

        public AdsSearchCriteria Next(DateTime searchExecutionDate)
        {
            var next = new AdsSearchCriteria
            {
                PublicationDate = PublicationDate.AddDays(1)
            };
            if (next.PublicationDate > searchExecutionDate)
            {
                return this;
            }
            return next;
        }
    }
}
