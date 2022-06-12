using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SerpAnalysis.Core.Interfaces;

namespace SerpAnalysis.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchResult: ObservableObject
    {

        private IList<SearchResultLine> _rankingRecords;

        public SearchResult(SearchQueryWithEngine searchQueryWithEngine, string rawData)
        {
            SearchQuery = searchQueryWithEngine;
            RawData = rawData;
        }

        public SearchQueryWithEngine SearchQuery { get; }

        public IList<SearchResultLine> RankingRecords
        {
            get => _rankingRecords;
            set => SetProperty(ref _rankingRecords, value);
        }

        public string RawData { get; }

        private ICrawler C { get; set; }
    }
}
