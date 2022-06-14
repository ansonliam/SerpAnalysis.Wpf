using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SerpAnalysis.Core.Interfaces;

namespace SerpAnalysis.Test.Mock
{
    public class MockCrawlerIntegrationService : ICrawlerIntegrationService
    {
        public MockCrawlerIntegrationService(string filePath,string htmlContent, HttpStatusCode statusCode)
        {
            FilePath = filePath;
            HtmlContent = htmlContent;
            StatusCode = statusCode;
        }

        public string FilePath { get; set; }
        public string HtmlContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public async Task<HttpResponseMessage> GetHttpResponse(string url)
        {
            var r = new HttpResponseMessage();

            r.StatusCode = StatusCode;
            r.Content = new StringContent(HtmlContent);

            return r;
        }


        public static ICrawlerIntegrationService GetMockCrawlerIntegrationService()
        {
            var filePath = "TestSamples/ServiceTest/GoodSampleHtmlSourceFromCode/conveyancing software.html";
            return new MockCrawlerIntegrationService(filePath, File.ReadAllText(filePath), HttpStatusCode.OK);
        }

        public static ICrawlerIntegrationService GetMockTooManyRequestCrawlerIntegrationService()
        {
            var filePath = "TestSamples/ServiceTest/BadSameHtmlSources/too_many_requests_best_bike.html";
            return new MockCrawlerIntegrationService(filePath, File.ReadAllText(filePath), HttpStatusCode.TooManyRequests);
        }
        public static ICrawlerIntegrationService GetMockBadRequestCrawlerIntegrationService()
        {
            var filePath = "TestSamples/ServiceTest/BadSameHtmlSources/too_many_requests_best_bike.html";
            return new MockCrawlerIntegrationService(filePath, File.ReadAllText(filePath), HttpStatusCode.BadRequest);
        }
    }
}
