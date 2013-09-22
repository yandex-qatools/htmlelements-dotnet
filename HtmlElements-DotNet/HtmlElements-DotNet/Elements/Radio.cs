using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.HtmlElements.Elements
{
    public class Radio : TypifiedElement
    {
        public Radio(IWebElement element)
            : base(element)
        { }

        public IList<IWebElement> GetButtons()
        {
            String radioName = WrappedElement.GetAttribute("name");

            String xpath;
            if (radioName == null)
            {
                xpath = "self::* | following::input[@type = 'radio'] | preceding::input[@type = 'radio']";
            }
            else
            {
                xpath = string.Format(
                        "self::* | following::input[@type = 'radio' and @name = '{0}'] | " +
                                "preceding::input[@type = 'radio' and @name = '{0}']",
                        radioName);
            }

            return WrappedElement.FindElements(By.XPath(xpath));
        }

        public IWebElement GetSelectedButton()
        {
            foreach (IWebElement button in GetButtons())
            {
                if (button.Selected)
                {
                    return button;
                }
            }

            throw new NoSuchElementException("No selected button");
        }

        public bool HasSelectedButton()
        {
            foreach (IWebElement button in GetButtons())
            {
                if (button.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        public void SelectByValue(string value)
        {
            foreach (IWebElement button in GetButtons())
            {
                string buttonValue = button.GetAttribute("value");
                if (value == buttonValue)
                {
                    SelectButton(button);
                    return;
                }
            }
            throw new NoSuchElementException(string.Format("Cannot locate radio button with value: {0}", value));
        }

        public void SelectByIndex(int index)
        {
            IList<IWebElement> buttons = GetButtons();

            if (index < 0 || index >= buttons.Count)
            {
                throw new NoSuchElementException(string.Format("Cannot locate radio button with index: {0}", index));
            }

            SelectButton(buttons[index]);
        }

        private void SelectButton(IWebElement button)
        {
            if (!button.Selected)
            {
                button.Click();
            }
        }
    }
}
