using System.Runtime.CompilerServices;
using NLog;
using SerpAnalysis.Core.Interfaces;

[assembly: InternalsVisibleTo("SerpAnalysis.Test")]
namespace SerpAnalysis.Core.BusinessServices
{
    public class CrawlerIntegrationService : ICrawlerIntegrationService
    {
        public async Task<HttpResponseMessage> GetHttpResponse(string url)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            LogManager.GetCurrentClassLogger().Debug($"Making HTTP Get Request using Url: {url}");
            var response = await client.GetAsync(url);

            LogManager.GetCurrentClassLogger().Debug($"Response Status Code returned: {response.StatusCode}");
            return response;
        }
    }
}
