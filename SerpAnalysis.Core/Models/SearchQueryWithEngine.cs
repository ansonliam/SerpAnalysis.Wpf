namespace SerpAnalysis.Core.Models
{
    public class SearchQueryWithEngine : ModelBase
    {
        private SearchQuery _query;
        private SearchEngine _engine;

        public SearchQueryWithEngine(SearchQuery query, SearchEngine engine)
        {
            Query = query;
            Engine = engine;
        }

        public SearchQuery Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        public SearchEngine Engine
        {
            get => _engine;
            set => SetProperty(ref _engine, value);
        }

        public SearchResult SearchResult { get; set; }
    }
}
