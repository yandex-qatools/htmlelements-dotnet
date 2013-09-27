using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.HtmlElements.Elements
{
    public class Select : TypifiedElement
    {
        public Select(IWebElement element)
            : base(element)
        { }

        private SelectElement GetSelect()
        {
            return new SelectElement(WrappedElement);
        }

        public bool IsMultiple
        {
            get { return GetSelect().IsMultiple; }
        }

        public IList<IWebElement> Options
        {
            get { return GetSelect().Options; }
        }

        public IList<IWebElement> AllSelectedOptions
        {
            get { return GetSelect().AllSelectedOptions; }
        }

        public IWebElement SelectedOption()
        {
            return GetSelect().SelectedOption;
        }

        public bool HasSelectedOption()
        {
            foreach (IWebElement option in Options)
            {
                if (option.Selected)
                {
                    return true;
                }
            }

            return false;
        }

        public void SelectByText(String text)
        {
            GetSelect().SelectByText(text);
        }

        public void SelectByIndex(int index)
        {
            GetSelect().SelectByIndex(index);
        }

        public void SelectByValue(string value)
        {
            GetSelect().SelectByValue(value);
        }

        public void DeselectAll()
        {
            GetSelect().DeselectAll();
        }

        public void DeselectByValue(String value)
        {
            GetSelect().DeselectByValue(value);
        }

        public void DeselectByIndex(int index)
        {
            GetSelect().DeselectByIndex(index);
        }

        public void DeselectByText(String text)
        {
            GetSelect().DeselectByText(text);
        }
    }
}
