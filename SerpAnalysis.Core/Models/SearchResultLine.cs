namespace SerpAnalysis.Core.Models
{
    public class SearchResultLine : ModelBase
    {
        private string _resultUrl;
        private int _ranking;

        public SearchResult Parent { get; }

        public string ResultUrl
        {
            get => _resultUrl;
            set => SetProperty(ref _resultUrl, value);
        }

        public int Ranking
        {
            get => _ranking;
            set => SetProperty(ref _ranking, value);
        }
    }
}
