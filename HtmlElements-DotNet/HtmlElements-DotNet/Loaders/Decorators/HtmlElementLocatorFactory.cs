using OpenQA.Selenium;
using System;
using System.Reflection;
using Yandex.HtmlElements.PageFactories;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.Loaders.Decorators
{
    // Done
    public class HtmlElementLocatorFactory : CustomElementLocatorFactory
    {
        private ISearchContext searchContext;

        public HtmlElementLocatorFactory(ISearchContext searchContext)
        {
            this.searchContext = searchContext;
        }

        public override IElementLocator CreateLocator(FieldInfo field)
        {
            return new AjaxElementLocator(searchContext, new HtmlElementFieldAttribytesHandler(field));
        }

        public override IElementLocator CreateLocator(Type type)
        {
            return new AjaxElementLocator(searchContext, new HtmlElementTypeAttributesHandler(type));
        }
    }
}
