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
    public class SearchQuery : ModelBase
    {
        private string _companyDomain;
        private string _keyword;
        private int _searchResultCount;

        public SearchQuery(string keyword, string companyDomain)
        {
            Keyword = keyword;
            CompanyDomain = companyDomain;
        }

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value);
        }

        public string CompanyDomain
        {
            get => _companyDomain;
            set
            {
                var domain = value.Trim().ToLower().Replace("https://", "");
                domain = domain.Trim().ToLower().Replace("/", "");

                if (domain.Length > 4 && domain.Substring(0, 4) == "www.")
                {
                    domain = domain.Substring(4, domain.Length - 4);
                }

                SetProperty(ref _companyDomain, domain);
            }
        }
    }
}
