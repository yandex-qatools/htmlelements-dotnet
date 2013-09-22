using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.Elements
{
    public class Form : TypifiedElement
    {
        private const string TextInputType = "text";
        private const string PasswordInputType = "password";
        private const string CheckboxType = "checkbox";
        private const string RadioType = "radio";

        public Form(IWebElement element)
            : base(element)
        {
        }

        public void Fill(IDictionary<string, object> data)
        {
            foreach (string key in data.Keys)
            {
                IWebElement elementToFill = FindElementByKey(key);
                if (elementToFill != null)
                {
                    FillElement(elementToFill, data[key]);
                }
            }
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }

        private IWebElement FindElementByKey(string key)
        {
            IList<IWebElement> elements = WrappedElement.FindElements(By.Name(key));
            if (elements.Count <= 0)
            {
                return null;
            }
            return elements[0];
        }

        protected void FillElement(IWebElement element, object value)
        {
            if (value == null)
            {
                return;
            }

            if (IsInput(element))
            {
                String inputType = element.GetAttribute("type");
                if (inputType == null || inputType == TextInputType || inputType == PasswordInputType)
                {
                    element.SendKeys(value.ToString());
                }
                else if (inputType == CheckboxType)
                {
                    CheckBox checkBox = new CheckBox(element);
                    checkBox.Set(bool.Parse(value.ToString()));
                }
                else if (inputType == RadioType)
                {
                    Radio radio = new Radio(element);
                    radio.SelectByValue(value.ToString());
                }
            }
            else if (IsSelect(element))
            {
                Select select = new Select(element);
                select.SelectByValue(value.ToString());
            }
            else if (IsTextArea(element))
            {
                element.SendKeys(value.ToString());
            }
        }

        private bool IsInput(IWebElement element)
        {
            return "input" == element.TagName;
        }

        private bool IsSelect(IWebElement element)
        {
            return "select" == element.TagName;
        }

        private bool IsTextArea(IWebElement element)
        {
            return "textarea" == element.TagName;
        }

    }
}
