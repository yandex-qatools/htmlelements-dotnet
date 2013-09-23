using ImpromptuInterface;
using OpenQA.Selenium;
using System.Dynamic;

namespace Yandex.HtmlElements.PageFactories.Selenium.ProxyHandlers
{
    public class WebElementProxyHandler : DynamicObject
    {
        private IElementLocator locator;

        private WebElementProxyHandler(IElementLocator locator)
            : base()
        {
            this.locator = locator;
        }

        public static IWebElement newInstance(IElementLocator locator)
        {
            return new WebElementProxyHandler(locator).ActLike<IWebElement>();
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
            return locator.FindElement().ToString();
        }
    }
}
