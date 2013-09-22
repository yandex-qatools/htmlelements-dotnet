using OpenQA.Selenium;

namespace Yandex.HtmlElements.Elements
{
    public class Button : TypifiedElement
    {
        public Button(IWebElement element) : base(element)
        {
        }

        public void Click()
        {
            WrappedElement.Click();
        }
    }
}
