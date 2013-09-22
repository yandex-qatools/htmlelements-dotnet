using OpenQA.Selenium;

namespace Yandex.HtmlElements.Elements
{
    public class Link : TypifiedElement
    {
        public Link(IWebElement element)
            : base(element)
        { }

        public string GetReference()
        {
            return WrappedElement.GetAttribute("href");
        }

        public void Click()
        {
            WrappedElement.Click();
        }

        public string Text
        {
            get { return WrappedElement.Text; }
        }
    }
}
