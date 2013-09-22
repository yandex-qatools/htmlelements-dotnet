using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.Elements
{
    public abstract class TypifiedElement : IWrapsElement, INamed
    {
        private readonly IWebElement wrappedElement;
        private string name;

        public TypifiedElement(IWebElement element)
        {
            this.wrappedElement = element;
        }

        public IWebElement WrappedElement
        {
            get { return wrappedElement; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Displayed
        {
            get { return wrappedElement.Displayed; }
        }

        public bool Enabled
        {
            get { return wrappedElement.Enabled; }
        }

        public bool Selected
        {
            get { return wrappedElement.Selected; }
        }
    }
}
