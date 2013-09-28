using HtmlElements.Tests.TestElements;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Loaders;
using Yandex.HtmlElements.PageFactories;

namespace HtmlElements.Tests.TestPages
{
    public class SearchPage
    {
        public const string LogoName = "Logotype";
        public const string LogoClass = "b-logo";

        private SearchArrow searchArrow;

        [Name(LogoName)]
        [FindsBy(How = How.ClassName, Using = LogoClass)]
        private IWebElement logo;

        public SearchPage()
            : this(MockDriver())
        {
        }

        public SearchPage(CustomElementLocatorFactory elementLocatorFactory)
        {
            HtmlElementLoader.PopulatePageObject(this, elementLocatorFactory);
        }

        public SearchPage(IWebDriver driver)
        {
            HtmlElementLoader.PopulatePageObject(this, driver);
        }

        public SearchArrow SearchArrow
        {
            get { return searchArrow; }
        }

        public IWebElement Logo
        {
            get { return logo; }
        }

        public static IWebDriver MockDriver()
        {
            Mock<IWebDriver> driver = new Mock<IWebDriver>();
            Mock<IWebElement> logo = new Mock<IWebElement>();
            Mock<IWebElement> searchArrow = new Mock<IWebElement>();

            Mock<IWebElement> searchInput = new Mock<IWebElement>();
            Mock<IWebElement> searchInputReloaded = new Mock<IWebElement>();

            Mock<IWebElement> searchButton = new Mock<IWebElement>();
            Mock<IWebElement> searchButtonReloaded = new Mock<IWebElement>();

            Mock<IWebElement> searchRequestSample = new Mock<IWebElement>();

            Mock<IWebElement> item1 = new Mock<IWebElement>();
            Mock<IWebElement> item2 = new Mock<IWebElement>();

            List<IWebElement> suggestItems = new List<IWebElement>();
            suggestItems.Add(item1.Object);
            suggestItems.Add(item2.Object);

            driver.Setup(drv => drv.FindElement(By.ClassName(LogoClass))).Returns(logo.Object);
            driver.Setup(drv => drv.FindElement(By.ClassName(SearchArrowData.SearchArrowClass))).Returns(searchArrow.Object);

            searchArrow.Setup(sa => sa.FindElement(By.ClassName(SearchArrowData.RequestInputClass))).Returns(searchInput.Object);
            searchArrow.Setup(sa => sa.FindElement(By.ClassName(SearchArrowData.RequestInputClass))).Returns(searchInputReloaded.Object);

            /** non-cached element reloads on page refresh */
            searchArrow.Setup(sa => sa.FindElement(By.ClassName(SearchArrowData.SearchButtonClass))).Returns(searchButton.Object);
            searchArrow.Setup(sa => sa.FindElement(By.ClassName(SearchArrowData.SearchButtonClass))).Returns(searchButtonReloaded.Object);

            searchArrow.Setup(sa => sa.FindElements(By.ClassName(SearchArrowData.SuggestClass))).Returns(suggestItems.AsReadOnly());
            item1.Setup(si => si.Text).Returns("yandex maps");
            item2.Setup(si => si.Text).Returns("yandex");

            searchArrow.Setup(sa => sa.FindElement(By.ClassName(SearchArrowData.SearchRequestSampleClass))).Returns(searchRequestSample.Object);

            return driver.Object;
        }
    }
}
