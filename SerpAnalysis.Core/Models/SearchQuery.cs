using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.CommonServices;

namespace SerpAnalysis.Core.Models
{
    public class SearchQuery : ObservableObject
    {
        private string _companyDomain;
        private string _keyword;
        private int _searchResultCount;

        public SearchQuery(string keyword, string companyDomain)
        {
            _keyword = keyword;
            _companyDomain = companyDomain;
        }

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value);
        }

        public string CompanyDomain
        {
            get => _companyDomain;
            set => SetProperty(ref _companyDomain, value);
        }
    }
}
