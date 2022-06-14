using System.Diagnostics;
using System.Net;
using System.Text;
using SerpAnalysis.Core.CommonServices;
using SerpAnalysis.Core.Interfaces;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.BusinessServices
{
    public class GoogleCrawler : IGoogleCrawler
    {
        //TODO: crawl the page and get the result 
        public async Task<(bool IsSuccessful, HttpStatusCode StatusCode, SearchResult SearchResult, string ResponseContent)> 
            Search(SearchQueryWithEngine searchQueryWithEngine, ICrawlerIntegrationService s)
        {
            var response = await s.GetHttpResponse(searchQueryWithEngine.EncodeUrlWithKeywords());


            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorContent);
                return (false, response.StatusCode, null, errorContent);
            }

            var content = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine(content);

            try
            {
                var rankings = ReturnRankings(content, searchQueryWithEngine);
                return (true, response.StatusCode, rankings, "");
            }
            catch (Exception e)
            {
                return (false, HttpStatusCode.BadRequest, null, e.Message);
            }
        }


        private SearchResult ReturnRankings(string rawHttpBody, SearchQueryWithEngine se)
        {
            var keyword = "<a href=\"/url?q="; //<a href="/url?q=
            var urlEndingKeyword = "&amp;";
            if (!rawHttpBody.Contains(keyword) || !rawHttpBody.Contains(urlEndingKeyword))
            {
                throw new Exception($"Html body is not correct. Google probably changed their HTML Structure. Details: It does not include \"{keyword}\" AND \"{urlEndingKeyword}\"");
            }


            var relatedSearchIndex = rawHttpBody.IndexOf("\">Related searches</");

            IList<SearchResultLine> l = new List<SearchResultLine>();
            var ranking = 1;
            for (int index = 0; ; index += keyword.Length)
            {
                index = rawHttpBody.IndexOf(keyword, index);
                if (index == -1)
                    break;

                var urlStartIndex = index + keyword.Length;
                var urlLength = rawHttpBody.IndexOf("&amp;", urlStartIndex) - urlStartIndex;
                var url = rawHttpBody.Substring(urlStartIndex, urlLength);

                if (relatedSearchIndex >= 0 && urlStartIndex > relatedSearchIndex) //any urls below related search section is not relevant.
                    continue;


                var item = new SearchResultLine();
                item.Ranking = ranking++;
                item.ResultUrl = url;
                l.Add(item);
                Debug.WriteLine($"Ranking: {item.Ranking}, Url: {item.ResultUrl}");
            }


            var result = new SearchResult(se, rawHttpBody);
            result.RankingRecords = l;

            return result;
        }
    }
}