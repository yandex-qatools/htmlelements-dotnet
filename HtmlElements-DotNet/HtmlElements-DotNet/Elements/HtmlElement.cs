using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Yandex.HtmlElements.Elements
{
    public class HtmlElement : IWebElement, IWrapsElement, INamed
    {
        private IWebElement wrappedElement;
        private string name;

        public IWebElement WrappedElement
        {
            get { return wrappedElement; }
            set { wrappedElement = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void Click()
        {
            wrappedElement.Click();
        }

        public void Submit()
        {
            wrappedElement.Submit();
        }

        public void SendKeys(string keys)
        {
            wrappedElement.SendKeys(keys);
        }

        public void Clear()
        {
            wrappedElement.Clear();
        }

        public string TagName
        {
            get { return wrappedElement.TagName; }
        }

        public string GetAttribute(string name)
        {
            return wrappedElement.GetAttribute(name);
        }

        public bool Selected
        {
            get { return wrappedElement.Selected; }
        }

        public bool Enabled
        {
            get { return wrappedElement.Enabled; }
        }

        public string Text
        {
            get { return wrappedElement.Text; }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return wrappedElement.FindElements(by);
        }

        public IWebElement FindElement(By by)
        {
            return wrappedElement.FindElement(by);
        }

        public bool Displayed
        {
            get { return wrappedElement.Displayed; }
        }

        public Point Location
        {
            get { return wrappedElement.Location; }
        }

        public Size Size
        {
            get { return wrappedElement.Size; }
        }

        public string GetCssValue(string name)
        {
            return wrappedElement.GetCssValue(name);
        }

        public override String ToString()
        {
            return name;
        }
    }
}
