
using HtmlElements.Test.Browsers;
using HtmlElements.Test.Services.GoogleScreens;
using System;
using System.Collections.Generic;

namespace HtmlElements.Test.Services
{
    public class GoogleGuiService
    {
        private Browser browser;
        private string baseUrl;
        private MainPage mainPage;

        public GoogleGuiService(Browser browser)
        {
            this.browser = browser;
        }

        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
            set
            {
                baseUrl = value;
            }
        }

        public IList<string> Search(string query)
        {
            if (mainPage == null)
            {
                mainPage = new MainPage(browser, baseUrl);
            }
            mainPage.Open();
            SearchPage ss = mainPage.search(query);
            return ss.GetSearchResluts();
        }

        internal void SearchAndNavigateToImage(string query)
        {
            if (mainPage == null)
            {
                mainPage = new MainPage(browser, baseUrl);
            }
            mainPage.Open();
            SearchPage ss = mainPage.search(query);
            ss.OpenImageSearchPage();
        }
    }
}
