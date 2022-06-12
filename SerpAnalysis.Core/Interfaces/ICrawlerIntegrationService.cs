using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.CommonServices;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.Interfaces
{
    public interface ICrawlerIntegrationService
    {
        public Task<HttpResponseMessage> GetHttpResponse(string url);
    }
}
