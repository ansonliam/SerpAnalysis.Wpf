using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Test
{
    public class IntergrationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestCrawlerIntegrationService()
        {
            var records = CommonServiceTest.GetRecordsFromCsv("TestSamples/ServiceTest/TestCrawlerIntegrationService.csv");

            var currIndex = 0;
            var recordCount = records.Count;
            foreach (var record in records)
            {
                currIndex++;
                var sq = new SearchQuery(record.SearchTerm, record.UrlDomain);
                var se = BsUtilities.GetGoogleAuSearchEngine();
                var sqWithEngine = new SearchQueryWithEngine(sq, se);

                var s = new CrawlerIntegrationService();
                var res = await s.GetHttpResponse(sqWithEngine.EncodeUrlWithKeywords());

                var content = await res.Content.ReadAsStringAsync();

                Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

                if (currIndex < recordCount) //skip the sleep for the last record
                {
                    Thread.Sleep(5000); // sleep 5 seconds before calling Google to reduce the chance of being detected as bot 
                }
            }
        }
    }
}