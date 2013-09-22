using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.PageFactories
{
    // Done
    public class DefaultElementLocator : IElementLocator
    {
        private readonly ISearchContext searchContext;
        private readonly bool shouldCache;
        private readonly By by;
        private IWebElement cachedElement;
        private ReadOnlyCollection<IWebElement> cachedElementList;


        public DefaultElementLocator(ISearchContext searchContext, AttributesHandler attributesHandler)
        {
            this.searchContext = searchContext;
            this.shouldCache = attributesHandler.ShouldCache();
            this.by = attributesHandler.BuildBy();
        }
        public virtual IWebElement FindElement()
        {
            if (cachedElement != null && shouldCache)
            {
                return cachedElement;
            }

            IWebElement element = searchContext.FindElement(by);
            if (shouldCache)
            {
                cachedElement = element;
            }

            return element;
        }

        public virtual ReadOnlyCollection<IWebElement> FindElements()
        {
            if (cachedElementList != null && shouldCache)
            {
                return cachedElementList;
            }

            ReadOnlyCollection<IWebElement> elements = searchContext.FindElements(by);
            if (shouldCache)
            {
                cachedElementList = elements;
            }

            return elements;
        }
    }
}
