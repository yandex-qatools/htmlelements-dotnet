using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Yandex.HtmlElements.PageFactories.Selenium.ProxyHandlers;

namespace Yandex.HtmlElements.PageFactories.Selenium
{
    public class DefaultFieldDecorator : IFieldDecorator
    {
        protected IElementLocatorFactory factory;

        public DefaultFieldDecorator(IElementLocatorFactory locatorFactory)
        {
            this.factory = locatorFactory;
        }

        public virtual object Decorate(FieldInfo field)
        {
            if (!(typeof(IWebElement).IsAssignableFrom(field.FieldType)
                  || IsDecoratableList(field)))
            {
                return null;
            }

            IElementLocator locator = factory.CreateLocator(field);
            if (locator == null)
            {
                return null;
            }

            if (typeof(IWebElement).IsAssignableFrom(field.FieldType))
            {
                return proxyForLocator(locator);
            }
            else if (typeof(IList).IsAssignableFrom(field.FieldType))
            {
                return proxyForListLocator(locator);
            }
            else
            {
                return null;
            }
        }

        private bool IsDecoratableList(FieldInfo field)
        {
            Type type = field.FieldType;
            if (!typeof(IList<>).IsAssignableFrom(type))
            {
                return false;
            }

            Type listType = type.GenericTypeArguments[0];

            if (typeof(IWebElement) != listType)
            {
                return false;
            }

            if (field.GetCustomAttributes(typeof(FindsByAttribute), false).Length == 0)
            {
                return false;
            }
            return true;
        }

        private object proxyForLocator(IElementLocator locator)
        {
            return new WebElementProxyHandler(locator).Wrap();
        }

        private object proxyForListLocator(IElementLocator locator)
        {
            return new WebElementListProxyHandler(locator).Wrap();
        }

    }
}
