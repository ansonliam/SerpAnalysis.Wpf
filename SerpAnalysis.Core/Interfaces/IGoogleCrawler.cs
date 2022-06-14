using System.Net;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.Interfaces
{
    public interface IGoogleCrawler
    {
        public Task<(bool IsSuccessful, HttpStatusCode StatusCode, SearchResult SearchResult, string ResponseContent)> Search(SearchQueryWithEngine se, ICrawlerIntegrationService s);
    }
}
