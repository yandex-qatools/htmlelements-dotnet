using ImpromptuInterface;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Yandex.HtmlElements.PageFactories.Selenium.ProxyHandlers
{
    public class WebElementListProxyHandler : DynamicObject
    {
        private IElementLocator locator;

        private WebElementListProxyHandler(IElementLocator locator)
            : base()
        {
            this.locator = locator;
        }

        public static IList<IWebElement> newInstance(IElementLocator locator)
        {
            return new WebElementListProxyHandler(locator).ActLike<IList<IWebElement>>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            IReadOnlyCollection<IWebElement> elements = locator.FindElements();

            try
            {
                result = elements.GetType().GetMethod(binder.Name).Invoke(elements, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            IReadOnlyCollection<IWebElement> elements = locator.FindElements();

            try
            {
                result = elements.GetType().GetProperty(binder.Name).GetValue(elements);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int index = (int)indexes[0];
            IReadOnlyCollection<IWebElement> elements = locator.FindElements();
            try
            {
                result = elements.ElementAt<IWebElement>(index);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return locator.FindElements().ToString();
        }
    }
}
