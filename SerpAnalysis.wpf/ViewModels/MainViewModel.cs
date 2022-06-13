using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;

namespace SerpAnalysis.wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ResetCommand = new RelayCommand(Reset);
        }


        public void Reset()
        {
            KeywordInput = "";
            DomainInput = "";
        }

        public IAsyncRelayCommand FindPositionCommand { get; }
        public IRelayCommand ResetCommand { get; }

        private string _keywordInput;
        private string _domainInput;

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
    }
}
