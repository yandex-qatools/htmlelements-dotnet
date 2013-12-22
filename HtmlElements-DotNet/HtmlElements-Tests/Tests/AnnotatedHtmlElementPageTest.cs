using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlElements.Tests.TestPages;

namespace HtmlElements.Tests
{
    [TestClass]
    public class AnnotatedHtmlElementPageTest
    {
        private AnnotatedHtmlElementPage page = new AnnotatedHtmlElementPage();

        [TestMethod]
        public void HtmlElementOnPageMustBeNotNull()
        {
            Assert.IsNotNull(page.Element, "HtmlElement on page shouldn't be null");
        }

        [TestMethod]
        public void HtmlElementWrappedElementMustBeNotNull()
        {
            Assert.IsNotNull(page.Element.WrappedElement, "Wrapped IWebElement in HtmlElement on page shouldn't be null");
        }

        [TestMethod]
        public void HtmlElementTagNameMustBeAsDeclared()
        {
            Assert.AreEqual(AnnotatedHtmlElementPage.ElementTagName, page.Element.TagName, "HtmlElement tag name should meet expectation");
        }

        [TestMethod]
        public void HtmlElementNameMustBePopulated()
        {
            Assert.AreEqual(AnnotatedHtmlElementPage.ElementName, page.Element.Name, "HtmlElement Name property should return declared value");
        }

        [TestMethod]
        public void HtmlElementToStringMethodMustReturnPopulatedValue()
        {
            Assert.AreEqual(AnnotatedHtmlElementPage.ElementName, page.Element.ToString(), "HtmlElement .ToString() method should return declared value");
        }
    }
}
