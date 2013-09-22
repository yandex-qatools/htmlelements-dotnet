using System;
using Spring.Testing.NUnit;
using Common.Logging;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace HtmlElements.Test.Tests
{
    [TestFixture]
    public class SampleTest : SeleniumTest
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(SampleTest));

        private readonly string[] keyphrases = { "test 1", "test 2", "test 3" };

        //[Test(Description = "A simple test which searches in google and checks the number of results")]
        public void TestMethod1()
        {
            _log.Info("TestMethod1");
            foreach (string key in keyphrases)
            {
                IList<string> result = googleService.Search(key);
                Assert.AreEqual(10, result.Count, "Check number of resluts on page");
            }
        }

        [Test(Description = "A simple test which searches in google web search and switches to image search page")]
        public void TestMethod2()
        {
            _log.Info("TestMethod2");
            googleService.SearchAndNavigateToImage("test");
        }
    }
}
