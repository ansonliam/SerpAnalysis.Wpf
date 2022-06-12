using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SerpAnalysis.Core.Models
{
    public class SearchResultLine : ObservableObject
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
