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
                foreach (var file in Directory.EnumerateFiles(("TestSamples/ServiceTest/GoodSampleHtmlSourceFromCode")))
                {
                    l.Add(new MockCrawlerIntegrationService(file, File.ReadAllText(file), HttpStatusCode.OK));
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

    }
}