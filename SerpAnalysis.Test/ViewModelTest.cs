using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using NuGet.Frameworks;
using NUnit.Framework;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Models;
using SerpAnalysis.Test.Mock;
using SerpAnalysis.wpf.ViewModels;

namespace SerpAnalysis.Test
{
    internal class ViewModelTest
    {

        [OneTimeSetUp]
        public void Setup()
        {
            MockIoc.InitialiseIoc();
        }

        [Test]
        public void TestMainVmResetAll()
        {
            var vm = new MainViewModel();
            vm.ResetAll();

            Assert.AreEqual("", vm.KeywordInput);
            Assert.AreEqual("", vm.DomainInput);
        }

        [Test]
        public void TestMainVmResetResult()
        {
            var vm = new MainViewModel();
            vm.ResetResult();

            Assert.AreEqual(Enumerable.Empty<SearchResultLine>(), vm.DataGridItems);
            Assert.AreEqual("", vm.ResultRankingStr);

        }



        [Test]
        public async Task TestMainVmFindPositionFunc()
        {
            var vm = new MainViewModel();
            vm.KeywordInput = "conveyacing software";
            vm.DomainInput = "www.smokeball.com.au";
            await vm.FindPositionCommand
                .ExecuteAsync(null); //routes to MockCrawlerIntegrationService which used a sample file.

            if (vm.DataGridItems.Count() <= 0)
                Assert.Fail("There should be items in the DataGridItems");
        }

        [Test]
        public async Task TestMainVmProperties()
        {
            var vm = new MainViewModel();
            var tester = new NotifyPropertyChangedTester(vm);


            var mockLines = new List<SearchResultLine>();
            mockLines.Add(new SearchResultLine() { Ranking = 1, ResultUrl = "test.com" });

            // change the value
            vm.KeywordInput = "test";
            vm.DomainInput = "www.test.com.au";
            vm.ResultRankingStr = "test";
            vm.DataGridItems = mockLines;
            Assert.AreEqual(4, tester.Changes.Count, 0, "Changes count was wrong.");
        }
    }
}
