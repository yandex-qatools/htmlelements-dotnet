using OpenQA.Selenium;
using System;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Loaders.Decorators;
using Yandex.HtmlElements.PageFactories;
using Yandex.HtmlElements.PageFactories.Selenium;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Loaders
{
    // Done
    public static class HtmlElementLoader
    {
        public static T Create<T>(IWebDriver driver)
        {
            object result;
            Type type = typeof(T);
            if (HtmlElementUtils.IsHtmlElement(type))
            {
                result = HtmlElementFactory.CreateHtmlElementInstance(type);
                PopulateHtmlElement((HtmlElement)result, new HtmlElementLocatorFactory(driver));
            }
            else
            {
                result = (T)HtmlElementFactory.CreatePageObjectInstance(type, driver);
                PopulatePageObject(result, new HtmlElementLocatorFactory(driver));
            }
            return (T)result;
        }

        public static void Populate(object instance, IWebDriver driver)
        {
            if (HtmlElementUtils.IsHtmlElement(instance))
            {
                HtmlElement htmlElement = (HtmlElement)instance;
                PopulateHtmlElement(htmlElement, driver);
            }
            else
            {
                PopulatePageObject(instance, driver);
            }
        }

        public static T CreateHtmlElement<T>(ISearchContext searchContext) where T : HtmlElement
        {
            T htmlElementInstance = (T)HtmlElementFactory.CreateHtmlElementInstance(typeof(T));
            PopulateHtmlElement(htmlElementInstance, new HtmlElementLocatorFactory(searchContext));
            return htmlElementInstance;
        }

        public static T CreatePageObject<T>(IWebDriver driver)
        {
            T page = (T)HtmlElementFactory.CreatePageObjectInstance(typeof(T), driver);
            PopulatePageObject(page, new HtmlElementLocatorFactory(driver));
            return page;
        }

        public static void PopulateHtmlElement(HtmlElement htmlElement, ISearchContext searchContext)
        {
            PopulateHtmlElement(htmlElement, new HtmlElementLocatorFactory(searchContext));
        }

        private static void PopulateHtmlElement(HtmlElement htmlElement,
            CustomElementLocatorFactory locatorFactory)
        {
            Type htmlElementType = htmlElement.GetType();

            // Create locator that will handle Block annotation
            IElementLocator locator = locatorFactory.CreateLocator(htmlElementType);

            // The next line from Java code located here till I find a solution to replace ClassLoader
            //ClassLoader htmlElementClassLoader = htmlElement.getClass().getClassLoader();

            // Initialize block with IWebElement proxy and set its name
            string elementName = HtmlElementUtils.GetElementName(htmlElementType);

            IWebElement elementToWrap = HtmlElementFactory.CreateNamedProxyForWebElement(locator, elementName);
            htmlElement.WrappedElement = elementToWrap;
            htmlElement.Name = elementName;
            // Initialize elements of the block
            PageFactory.InitElements(new HtmlElementDecorator(elementToWrap), htmlElement);
        }

        public static void PopulatePageObject(object page, IWebDriver driver)
        {
            PopulatePageObject(page, new HtmlElementLocatorFactory(driver));
        }

        public static void PopulatePageObject(object page,
            CustomElementLocatorFactory locatorFactory)
        {
            PageFactory.InitElements(new HtmlElementDecorator(locatorFactory), page);
        }
    }
}
