using System;
using System.Reflection;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.PageFactories
{
    // Done
    public abstract class CustomElementLocatorFactory : IElementLocatorFactory
    {
        public virtual IElementLocator CreateLocator(FieldInfo field)
        {
            return null;
        }

        public virtual IElementLocator CreateLocator(Type clazz)
        {
            return null;
        }
    }
}
