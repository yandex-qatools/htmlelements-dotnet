using ImpromptuInterface;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.Loaders.Decorators.ProxyHandlers
{
    public class WebElementListNamedProxyHandler : DynamicObject
    {
        private IElementLocator locator;
        private string name;

        public WebElementListNamedProxyHandler(IElementLocator locator, string name)
            : base()
        {
            this.locator = locator;
            this.name = name;
        }

        public IList<IWebElement> Wrap()
        {
            return this.ActLike<IList<IWebElement>>();
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
            return name;
        }
    }
}
