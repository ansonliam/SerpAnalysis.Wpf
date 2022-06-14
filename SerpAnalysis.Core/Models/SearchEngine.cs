using SerpAnalysis.Core.CommonServices;

namespace SerpAnalysis.Core.Models
{
    public class SearchEngine : ModelBase
    {
        /// <summary>
        /// If you need GoogleAu SearchEngine object, You can use BsUtilities.GetGoogleAuSearchEngine()
        /// </summary>
        /// <param name="country"></param>
        /// <param name="provider"></param>
        /// <param name="searchEngineDomain"></param>
        /// <param name="parameters"></param>
        public SearchEngine(Country country, Provider provider, string searchEngineDomain, SearchEngineParameter parameters)
        {
            Country = country;
            Provider = provider;

            ProviderWithCountry = CsUtilities.GetEnum<Country, Provider, SearchEngineWithCountry>(country, provider);
            Domain = searchEngineDomain;
            Parameters = parameters;
        }

        public Country Country { get; set; }
        public Provider Provider { get; set; }
        public SearchEngineWithCountry ProviderWithCountry { get; set; }
        public string Domain { get; set; }

        public int MaxSearchResultCount
        {
            get
            {
                if (Provider == Provider.Google)
                    return 100;
                else
                    return 50; //bing and others are 50
            }
        }

        public SearchEngineParameter Parameters { get; set; }
    }




    public enum Provider
    {
        Google,
        Bing
    }


    /// <summary>
    /// The name in this Enum collection is a combination of 
    /// </summary>
    public enum SearchEngineWithCountry
    {
        GoogleAu,
        //GoogleJP (To be expanded)
        //GoogleAT (To be expanded)
        //Bing (To be expanded)
    }

    public enum Country
    {
        Au,
        //JP
        //AT
    }
}
