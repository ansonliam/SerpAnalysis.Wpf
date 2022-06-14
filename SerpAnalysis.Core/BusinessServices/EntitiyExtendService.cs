using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.BusinessServices
{
    public static class EntityExtendService
    {
        public static string EncodeUrlWithKeywords(this SearchQueryWithEngine se)
        {
            var p = se.Engine.Parameters;
            var url = $"https://{se.Engine.Domain}/search?{p.Count}={se.Engine.MaxSearchResultCount}&{p.QueryParameterName}={Uri.EscapeDataString(se.Query.Keyword)}";
            return url;
        }
    }
}
