using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Yandex.HtmlElements.PageFactories.Selenium
{
    public interface IElementLocator
    {
        IWebElement FindElement();
        ReadOnlyCollection<IWebElement> FindElements();
    }
}
