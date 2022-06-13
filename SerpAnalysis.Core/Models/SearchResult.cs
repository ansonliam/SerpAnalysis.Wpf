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
    public class SearchResult : ModelBase
    {


        public SearchResult(SearchQueryWithEngine searchQueryWithEngine, string rawData)
        {
            SearchQuery = searchQueryWithEngine;
            RawData = rawData;
        }

        public string FilterRankingsBasedOnDomain()
        {
            var domain = SearchQuery.Query.CompanyDomain;

            var records = RankingRecords.Where(x => x.ResultUrl.Contains(domain)).Select(x=> x.Ranking);
            var result = String.Join(", ", records.ToArray());

            if (result == "")
                return "0";
            return result;
        }



        private IList<SearchResultLine> _rankingRecords;


        public SearchQueryWithEngine SearchQuery { get; }

        public IList<SearchResultLine> RankingRecords
        {
            get => _rankingRecords;
            set => SetProperty(ref _rankingRecords, value);
        }

        public string RawData { get; }
    }
}
