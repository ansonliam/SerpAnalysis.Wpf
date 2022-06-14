namespace SerpAnalysis.Core.Interfaces
{
    public interface ICrawlerIntegrationService
    {
        public Task<HttpResponseMessage> GetHttpResponse(string url);
    }
}
