using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestCompanyDomain()
        {
            var domain = "smokeballwww.com.au";


            var list = new List<SearchQuery>();



            list.Add(new SearchQuery("conveyancing software", " https://www.smokeballwww.com.au "));
            list.Add(new SearchQuery("conveyancing software", "www.smokeballwww.com.au "));
            list.Add(new SearchQuery("conveyancing software", "www.Smokeballwww.com.au"));
            list.Add(new SearchQuery("conveyancing software", "WWW.smokeballwww.com.au"));

            foreach (var searchQuery in list)
            {
                Assert.AreEqual(domain, searchQuery.CompanyDomain);
            }
        }
    }
}
