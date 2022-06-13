using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SerpAnalysis.Core.Interfaces;

namespace SerpAnalysis.Test.Mock
{
    public class MockCrawlerIntegrationService : ICrawlerIntegrationService
    {
        public MockCrawlerIntegrationService(string filePath,string htmlContent)
        {
            FilePath = filePath;
            HtmlContent = htmlContent;
        }

        public string FilePath { get; set; }
        public string HtmlContent { get; set; }
        public async Task<HttpResponseMessage> GetHttpResponse(string url)
        {
            var r = new HttpResponseMessage();

            r.StatusCode = HttpStatusCode.OK;
            r.Content = new StringContent(HtmlContent);

            return r;
        }


        public static ICrawlerIntegrationService GetMockCrawlerIntegrationService()
        {
            var filePath = "TestSamples/ServiceTest/GoodSampleHtmlSourceFromCode/conveyancing software.html";
            return new MockCrawlerIntegrationService(filePath, File.ReadAllText(filePath));
        }
    }
}
