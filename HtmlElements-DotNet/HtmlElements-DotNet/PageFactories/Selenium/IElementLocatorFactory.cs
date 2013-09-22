using System.Reflection;

namespace Yandex.HtmlElements.PageFactories.Selenium
{
    public interface IElementLocatorFactory
    {
        IElementLocator CreateLocator(FieldInfo field);
    }
}
