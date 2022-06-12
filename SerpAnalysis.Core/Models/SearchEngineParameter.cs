namespace SerpAnalysis.Core.Models
{
    /// <summary>
    /// This class contains the parameters used by Search Engine in the url.
    /// </summary>
    public struct SearchEngineParameter
    {
        public SearchEngineParameter(string count, string queryParameterName = "q")
        {
            Count = count;
            QueryParameterName = queryParameterName;
        }

        /// <summary>
        /// The number of search results to return in the response.
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// The user's search query term. The term may not be empty.
        /// </summary>
        public string QueryParameterName { get; set; }
    }
}
