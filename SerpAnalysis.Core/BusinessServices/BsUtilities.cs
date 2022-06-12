using SerpAnalysis.Core.CommonServices;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Core.BusinessServices
{
    public class BsUtilities
    {
        public static SearchEngine GetGoogleAuSearchEngine()
        {
            return new SearchEngine(Country.Au, Provider.Google, "google.com.au", new Parameter("num"));
        }
    }
}
