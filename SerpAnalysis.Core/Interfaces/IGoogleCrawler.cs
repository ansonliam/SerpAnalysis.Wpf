using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.CommonServices;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.Interfaces
{
    public interface IGoogleCrawler
    {
        public Task<(bool IsSuccessful, HttpStatusCode StatusCode, SearchResult SearchResult, string ResponseContent)> Search(SearchQueryWithEngine se, ICrawlerIntegrationService s);
    }
}
