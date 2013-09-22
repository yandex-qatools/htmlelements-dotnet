using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using HtmlElements.Test.Browsers;
using HtmlElements.Test.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElements.Test.Services.GoogleScreens
{
    [Identity("XPath://*[@name='btnK']")]
    public class MainPage : BasicSearchScreen, IMainScreen
    {
        private Uri baseUrl;

        [FindsBy(How = How.XPath, Using = "//*[@name='q']")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//*[@name='btnK']")]
        private IWebElement searchButon;

        public MainPage(Browser browser, string baseUrl)
            : base(browser)
        {
            this.baseUrl = new Uri(baseUrl);
        }

        public override void Open()
        {
            browser.GetPage(this);
        }

        public SearchPage search(string query)
        {
            searchField.SendKeys(query);
            searchButon.Click();
            SearchPage ss = new SearchPage(browser);
            browser.Init(ss);
            return ss;
        }

        public Uri BaseUrl
        {
            get
            {
                return this.baseUrl;
            }
        }
    }
}
