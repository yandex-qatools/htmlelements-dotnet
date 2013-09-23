using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Exceptions;
using Yandex.HtmlElements.Loaders.Decorators.ProxyHandlers;
using Yandex.HtmlElements.PageFactories.Selenium;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Loaders.Decorators
{
    public class HtmlElementFactory
    {
        public static HtmlElement CreateHtmlElementInstance(Type elementType)
        {
            if (typeof(HtmlElement).IsAssignableFrom(elementType))
            {
                return HtmlElementUtils.newInstance<HtmlElement>(elementType);
            }
            throw new HtmlElementsException(string.Format("Type '{0}' isn't a derivative type of 'HtmlElement'", elementType));
        }

        public static TypifiedElement CreateTypifiedElementInstance(Type elementType, IWebElement elementToWrap)
        {
            if (typeof(TypifiedElement).IsAssignableFrom(elementType))
            {
                return HtmlElementUtils.newInstance<TypifiedElement>(elementType, elementToWrap);
            }
            throw new HtmlElementsException(string.Format("Type '{0}' isn't a derivative type of 'TypifiedElement'", elementType));
        }

        public static object CreatePageObjectInstance(Type type, IWebDriver driver)
        {
            return HtmlElementUtils.newInstance<object>(type, driver);
        }

        public static IWebElement CreateNamedProxyForWebElement(IElementLocator locator, string elementName)
        {
            return WebElementNamedProxyHandler.newInstance(locator, elementName);
        }

        internal static object CreateNamedProxyForWebElementList(IElementLocator locator, string listName)
        {
            return WebElementListNamedProxyHandler.newInstance(locator, listName);
        }

        internal static object CreateNamedProxyForTypifiedElementList(Type listType, Type elementType, IElementLocator locator, string listName)
        {
            return TypifiedElementListNamedProxyHandler.newInstance(listType, elementType, locator, listName);
        }

        internal static object CreateNamedProxyForHtmlElementList(Type listType, Type elementType, IElementLocator locator, string listName)
        {
            return HtmlElementListNamedProxyHandler.newInstance(listType, elementType, locator, listName);
        }

    }
}
