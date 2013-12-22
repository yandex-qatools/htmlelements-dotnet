using Common.Logging;
using HtmlElements.Test.Services.GoogleScreens;
using NUnit.Framework;
using System.Collections.Generic;

namespace HtmlElements.Test.Tests
{
    [TestFixture]
    public class SampleTest : SeleniumTest
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(SampleTest));

        private readonly string[] keyphrases = { "test 1", "test 2", "test 3" };

        [Test(Description = "A simple test which searches in google and checks the number of results")]
        public void TestMethod1()
        {
            Log.Info("Sample test with Search requests");
            foreach (string key in keyphrases)
            {
                IList<string> result = googleService.Search(key);
                Assert.AreEqual(10, result.Count, "Check number of results on page");
            }
        }

        [Test(Description = "A simple test which searches in google web search and switches to image search page")]
        public void TestMethod2()
        {
            Log.Info("Sample tests with screens");
            IList<string> result = googleService.Search("Test Search");
            ImageSearchPage isp = ((BasicSearchScreen)Browser.CurrentScreen).OpenImageSearchPage();
            Assert.NotNull(isp, "Check Image Page isn't null");
        }
    }
}
