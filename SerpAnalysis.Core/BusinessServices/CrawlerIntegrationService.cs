using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using NLog;
using SerpAnalysis.Core.Interfaces;
using SerpAnalysis.Core.Models;

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
