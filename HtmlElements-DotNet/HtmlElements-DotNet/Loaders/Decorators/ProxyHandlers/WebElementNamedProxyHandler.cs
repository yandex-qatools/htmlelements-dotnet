using ImpromptuInterface;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Dynamic;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.Loaders.Decorators.ProxyHandlers
{
    public class WebElementNamedProxyHandler : DynamicObject
    {
        private IElementLocator locator;
        private string name;

        private WebElementNamedProxyHandler(IElementLocator locator, string name)
            : base()
        {
            this.locator = locator;
            this.name = name;
        }

        public static IWebElement newInstance(IElementLocator locator, string name)
        {
            return new WebElementNamedProxyHandler(locator, name).ActLike<IWebElement>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            IWebElement element = locator.FindElement();
            try
            {
                result = element.GetType().GetMethod(binder.Name).Invoke(element, args);
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
            IWebElement element = locator.FindElement();

            if ("WrappedElement" == binder.Name)
            {
                result = element;
                return true;
            }

            try
            {
                result = element.GetType().GetProperty(binder.Name).GetValue(element);
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
