using OpenQA.Selenium;

namespace Yandex.HtmlElements.Elements
{
    public class CheckBox : TypifiedElement
    {
        public CheckBox(IWebElement element)
            : base(element)
        {
        }

        public IWebElement Label
        {
            get
            {
                try
                {
                    return WrappedElement.FindElement(By.XPath("following-sibling::label"));
                }
                catch
                {
                    return null;
                }
            }
        }

        public string LabelText
        {
            get
            {
                IWebElement label = Label;
                return label == null ? null : label.Text;
            }
        }

        public string Text
        {
            get
            {
                return LabelText;
            }
        }

        public void Select()
        {
            if (!Selected)
            {
                WrappedElement.Click();
            }
        }

        public void Deselect()
        {
            if (Selected)
            {
                WrappedElement.Click();
            }
        }

        public void Set(bool value)
        {
            if (value)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }
    }
}
