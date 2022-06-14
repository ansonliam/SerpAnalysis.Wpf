using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Interfaces;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ResetCommand = new RelayCommand(ResetAll);
            FindPositionCommand = new AsyncRelayCommand(FindPosition);
        }

        private async Task FindPosition()
        {
            ResetResult();
            var sq = new SearchQuery(KeywordInput, DomainInput);
            var engine = BsUtilities.GetGoogleAuSearchEngine();

            var se = new SearchQueryWithEngine(sq, engine);


            var crawler = Ioc.Default.GetService<IGoogleCrawler>();
            var integrationService = Ioc.Default.GetService<ICrawlerIntegrationService>();
            var result = await crawler.Search(se, integrationService);

            if (result.StatusCode != HttpStatusCode.OK)
                return;

            ResultRankingStr = result.SearchResult.FilterRankingsAsString();
            DataGridItems = result.SearchResult.FilterRankingsAsList();
        }

        public void ResetAll()
        {
            KeywordInput = "";
            DomainInput = "";
            ResetResult();
        }

        public void ResetResult()
        {
            DataGridItems = Enumerable.Empty<SearchResultLine>();
            ResultRankingStr = "";
        }



        public IAsyncRelayCommand FindPositionCommand { get; }
        public IRelayCommand ResetCommand { get; }

        private string _keywordInput = "conveyancing software";
        private string _domainInput = "www.smokeball.com.au";
        private string _resultRankingStr = "";
        private IEnumerable<SearchResultLine> _dataGridItems = new List<SearchResultLine>();

        public string KeywordInput
        {
            get => _keywordInput;
            set => SetProperty(ref _keywordInput, value);
        }

        public string DomainInput
        {
            get => _domainInput;
            set => SetProperty(ref _domainInput, value);
        }

        public string ResultRankingStr
        {
            get => _resultRankingStr;
            set => SetProperty(ref _resultRankingStr, value);
        }


        public IEnumerable<SearchResultLine> DataGridItems
        {
            get => _dataGridItems;
            set => SetProperty(ref _dataGridItems, value);
        }
    }
}
