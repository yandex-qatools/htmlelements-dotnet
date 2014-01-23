using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Loaders;

namespace HtmlElements.Tests.TestPages
{
    public class AnnotatedHtmlElementPage
    {
        public const string ElementClassName = "element";
        public const string ElementTagName = "div";
        public const string ElementName = "Name of AnnotatedHtmlElementPage element";

        [Name(ElementName)]
        [FindsBy(How = How.ClassName, Using = ElementClassName)]
        private HtmlElement element;

        public AnnotatedHtmlElementPage()
            : this(MockDriver())
        {
        }

        public AnnotatedHtmlElementPage(IWebDriver driver)
        {
            HtmlElementLoader.PopulatePageObject(this, driver);
        }

        public static IWebDriver MockDriver()
        {
            Mock<IWebDriver> mockedDriver = new Mock<IWebDriver>();
            Mock<IWebElement> mockedElement = new Mock<IWebElement>();

            mockedElement.Setup(el => el.TagName).Returns(ElementTagName);
            mockedDriver.Setup(wd => wd.FindElement(new ByChained(By.ClassName(ElementClassName)))).Returns(mockedElement.Object);

            return mockedDriver.Object;
        }

        public HtmlElement Element
        {
            get { return element; }
        }
    }
}
