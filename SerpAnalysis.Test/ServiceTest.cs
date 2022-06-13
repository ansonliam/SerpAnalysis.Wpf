using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NUnit.Framework;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Models;
using SerpAnalysis.Test.Mock;

namespace SerpAnalysis.Test
{
    public class ServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGoogleCrawlerSearchFromCode()
        {
            //mock
            try
            {
                var sq = new SearchQuery("conveyancing software", "smokeball.com.au");
                var se = BsUtilities.GetGoogleAuSearchEngine();
                var sqWithEngine = new SearchQueryWithEngine(sq, se);

                var l = new List<MockCrawlerIntegrationService>();
                foreach (var file in Directory.EnumerateFiles(("GoodSampleHtmlSourceFromCode")))
                {
                    l.Add(new MockCrawlerIntegrationService(file, File.ReadAllText(file)));
                }


                //test
                foreach (var service in l)
                {
                    var s = new GoogleCrawler();
                    var response = await s.Search(sqWithEngine, service);

                    Assert.IsNotNull(response.SearchResult);
                    Assert.IsNotNull(response.SearchResult.RankingRecords);
                    Assert.IsNotEmpty(response.SearchResult.RankingRecords);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Test]
        public async Task TestCrawlerIntegrationService()
        {
            var records = TestCommonService.GetRecordsFromCsv("TestSamples/ServiceTest/TestCrawlerIntegrationService.csv");

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