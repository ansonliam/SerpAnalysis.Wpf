using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using NLog;
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
            Log = App.GetLogger(this.GetType().Name);
        }

        private async Task FindPosition()
        {
            Log.Debug("FindPosition() Called");


            ResetResult();

            ResultRankingStr = "loading...";


            var sq = new SearchQuery(KeywordInput, DomainInput);
            var engine = BsUtilities.GetGoogleAuSearchEngine();
            var se = new SearchQueryWithEngine(sq, engine);


            var crawler = Ioc.Default.GetService<IGoogleCrawler>();
            var integrationService = Ioc.Default.GetService<ICrawlerIntegrationService>();

            //Log.Debug($"crawler.Search Start, {}:\"{KeywordInput}\", {nameof(DomainInput)}:\"{DomainInput}\", Classes Injected: {crawler.GetType().Name} & {integrationService.GetType().Name}");
            Log.ForDebugEvent().Message("crawler.Search Start")
                .Properties(new List<KeyValuePair<string, object>>()
                {
                    new(nameof(KeywordInput), KeywordInput),
                    new(nameof(DomainInput), DomainInput),
                    new("Crawler Class", crawler.GetType().Name),
                    new("Integration Class", integrationService.GetType().Name),
                }).Log();

            var result = await crawler.Search(se, integrationService);

            Log.Debug($"crawler.Search End, Result: {result.IsSuccessful}, {result.StatusCode}");


            if (result.StatusCode != HttpStatusCode.OK)
            {
                ResultRankingStr = result.ResponseContent;
            }
            else
            {
                ResultRankingStr = result.SearchResult.FilterRankingsAsString();
                DataGridItems = result.SearchResult.FilterRankingsAsList();
                Log.Debug($"Filtered Result String: {ResultRankingStr}");
            }


            LogManager.GetCurrentClassLogger().Debug("FindPosition() Ended");
        }


        private ILogger Log { get; }

        public void ResetAll()
        {
            Log.Debug("ResetAll() Called");
            KeywordInput = "";
            DomainInput = "";
            ResetResult();

            Log.Debug("ResetAll() Ended");
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
