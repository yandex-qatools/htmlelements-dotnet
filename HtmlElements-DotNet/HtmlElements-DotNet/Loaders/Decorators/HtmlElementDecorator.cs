using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Exceptions;
using Yandex.HtmlElements.PageFactories.Selenium;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Loaders.Decorators
{
    public class HtmlElementDecorator : DefaultFieldDecorator
    {
        public HtmlElementDecorator(ISearchContext searchContext)
            : this(new HtmlElementLocatorFactory(searchContext))
        { }

        public HtmlElementDecorator(IElementLocatorFactory locatorFactory)
            : base(locatorFactory)
        {
        }

        public override object Decorate(FieldInfo field)
        {
            if (!IsDecoratableField(field))
            {
                return null;
            }

            IElementLocator locator = factory.CreateLocator(field);
            if (locator == null)
            {
                return null;
            }

            String elementName = HtmlElementUtils.GetElementName(field);

            if (HtmlElementUtils.IsTypifiedElement(field))
            {
                Type typifiedElementType = field.FieldType;
                return DecorateTypifiedElement(typifiedElementType, locator, elementName);
            }
            else if (HtmlElementUtils.IsHtmlElement(field))
            {
                Type htmlElementType = field.FieldType;
                return DecorateHtmlElement(htmlElementType, locator, elementName);
            }
            else if (HtmlElementUtils.IsWebElement(field))
            {
                return DecorateWebElement(locator, elementName);
            }
            else if (HtmlElementUtils.IsTypifiedElementList(field))
            {
                Type typifiedElementType = HtmlElementUtils.GetGenericParameterType(field);
                return DecorateTypifiedElementList(field.FieldType, typifiedElementType, locator, elementName);
            }
            else if (HtmlElementUtils.IsHtmlElementList(field))
            {
                Type htmlElementType = HtmlElementUtils.GetGenericParameterType(field);
                return DecorateHtmlElementList(field.FieldType, htmlElementType, locator, elementName);
            }
            else if (HtmlElementUtils.IsWebElementList(field))
            {
                return DecorateWebElementList(locator, elementName);
            }

            return null;
        }

        private bool IsDecoratableField(FieldInfo field)
        {
            // TODO Protecting wrapped element from initialization basing on its name is unsafe. Think of a better way.
            if (HtmlElementUtils.IsWebElement(field) && field.Name != "wrappedElement")
            {
                return true;
            }

            return (HtmlElementUtils.IsWebElementList(field) || HtmlElementUtils.IsHtmlElement(field) || HtmlElementUtils.IsHtmlElementList(field) ||
                    HtmlElementUtils.IsTypifiedElement(field) || HtmlElementUtils.IsTypifiedElementList(field));
        }

        private TypifiedElement DecorateTypifiedElement(Type elementType, IElementLocator locator, string elementName)
        {
            // Create typified element and initialize it with WebElement proxy
            IWebElement elementToWrap = HtmlElementFactory.CreateNamedProxyForWebElement(locator, elementName);
            TypifiedElement typifiedElementInstance = HtmlElementFactory.CreateTypifiedElementInstance(elementType, elementToWrap);
            typifiedElementInstance.Name = elementName;
            return typifiedElementInstance;
        }

        private HtmlElement DecorateHtmlElement(Type elementType, IElementLocator locator, string elementName)
        {
            // Create block and initialize it with WebElement proxy
            IWebElement elementToWrap = HtmlElementFactory.CreateNamedProxyForWebElement(locator, elementName);
            HtmlElement htmlElementInstance = HtmlElementFactory.CreateHtmlElementInstance(elementType);
            htmlElementInstance.WrappedElement = elementToWrap;
            htmlElementInstance.Name = elementName;
            // Recursively initialize elements of the block
            PageFactory.InitElements(new HtmlElementDecorator(elementToWrap), htmlElementInstance);
            return htmlElementInstance;
        }

        private IWebElement DecorateWebElement(IElementLocator locator, string elementName)
        {
            return HtmlElementFactory.CreateNamedProxyForWebElement(locator, elementName);
        }

        private object DecorateTypifiedElementList(Type listType, Type elementType, IElementLocator locator, string listName)
        {
            return HtmlElementFactory.CreateNamedProxyForTypifiedElementList(listType, elementType, locator, listName);
        }

        private object DecorateHtmlElementList(Type listType, Type elementType, IElementLocator locator, string listName)
        {
            return HtmlElementFactory.CreateNamedProxyForHtmlElementList(listType, elementType, locator, listName);
        }

        private object DecorateWebElementList(IElementLocator locator, string listName)
        {
            return HtmlElementFactory.CreateNamedProxyForWebElementList(locator, listName);
        }
    }
}
