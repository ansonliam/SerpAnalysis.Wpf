using System;
using System.Collections.Generic;
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

namespace SerpAnalysis.Test
{
    internal class ModelTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCompanyDomain()
        {
            var list = new List<SearchQuery>();

            var records = CommonServiceTest.GetRecordsFromCsv("TestSamples/ModelTest/FakeCompanyDomains.csv");
            foreach (var record in records)
            {
                var s = new SearchQuery(record.SearchTermInput, record.UrlDomainInput);
                Assert.AreEqual(record.CompanyDomain, s.CompanyDomain);
            }
        }
    }
}
