using Moq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Elements;

namespace HtmlElements.Tests.TestElements
{
    public class MockedForm : Form
    {
        public const string TextInputName = "textinput";
        public const string CheckboxName = "checkbox";
        public const string SelectName = "select";
        public const string RadioName = "radio";
        public const string TextAreaName = "textarea";

        public const String RadioButtonValue = "value";
        public const String SelectOptionValue = "value";

        private readonly Mock<IWebElement> mockedElement;

        private readonly Mock<IWebElement> textInput;
        private readonly Mock<IWebElement> checkBox;
        private readonly Mock<IWebElement> selectOption;
        private readonly Mock<IWebElement> radioButton;
        private readonly Mock<IWebElement> textArea;

        public MockedForm(Mock<IWebElement> mockedElement)
            : base(mockedElement.Object)
        {
            this.mockedElement = mockedElement;
            textInput = MockTextInput();
            checkBox = MockCheckBox();
            selectOption = MockSelectOption();
            radioButton = MockRadioButton();
            textArea = MockTextArea();
        }

        private Mock<IWebElement> MockInputWithNameAndType(string name, string type)
        {
            Mock<IWebElement> element = new Mock<IWebElement>();
            element.Setup(m => m.TagName).Returns("input");
            element.Setup(m => m.GetAttribute("name")).Returns(name);
            element.Setup(m => m.GetAttribute("type")).Returns(type);
            return element;
        }

        private Mock<IWebElement> MockTextInput()
        {
            Mock<IWebElement> textInput = MockInputWithNameAndType(TextInputName, "text");
            ReadOnlyCollection<IWebElement> textInputList = new List<IWebElement> { textInput.Object }.AsReadOnly();
            mockedElement.Setup(m => m.FindElements(By.Name(TextInputName))).Returns(textInputList);
            return textInput;
        }

        private Mock<IWebElement> MockCheckBox()
        {
            Mock<IWebElement> checkBox = MockInputWithNameAndType(CheckboxName, "checkbox");
            ReadOnlyCollection<IWebElement> checkBoxList = new List<IWebElement> { checkBox.Object }.AsReadOnly();
            mockedElement.Setup(m => m.FindElements(By.Name(CheckboxName))).Returns(checkBoxList);
            return checkBox;
        }

        private Mock<IWebElement> MockRadioButton()
        {
            Mock<IWebElement> radioButton = MockInputWithNameAndType(RadioName, "radio");
            ReadOnlyCollection<IWebElement> radioList = new List<IWebElement> { radioButton.Object }.AsReadOnly();
            mockedElement.Setup(m => m.FindElements(By.Name(RadioName))).Returns(radioList);
            radioButton.Setup(m => m.FindElements(By.XPath(string.Format("self::* | following::input[@type = 'radio' " +
                "and @name = '{0}'] | preceding::input[@type = 'radio' and @name = '{0}']",
                RadioName)))).Returns(radioList);
            radioButton.Setup(m => m.GetAttribute("value")).Returns(RadioButtonValue);
            return radioButton;
        }

        private Mock<IWebElement> MockSelectOption()
        {
            Mock<IWebElement> select = new Mock<IWebElement>();
            Mock<IWebElement> selectOption = new Mock<IWebElement>();
            select.Setup(m => m.TagName).Returns("select");
            select.Setup(m => m.GetAttribute("name")).Returns(SelectName);
            ReadOnlyCollection<IWebElement> selectList = new List<IWebElement> { select.Object }.AsReadOnly();
            mockedElement.Setup(m => m.FindElements(By.Name(SelectName))).Returns(selectList);
            ReadOnlyCollection<IWebElement> selectOptionList = new List<IWebElement> { selectOption.Object }.AsReadOnly();
            select.Setup(m => m.FindElements(By.XPath(string.Format(".//option[@value = \"{0}\"]", SelectOptionValue)))).Returns(selectOptionList);
            return selectOption;
        }

        private Mock<IWebElement> MockTextArea()
        {
            Mock<IWebElement> textArea = new Mock<IWebElement>();
            ReadOnlyCollection<IWebElement> textAreaList = new List<IWebElement> { textArea.Object }.AsReadOnly();
            mockedElement.Setup(m => m.FindElements(By.Name(TextAreaName))).Returns(textAreaList);
            textArea.Setup(m => m.TagName).Returns("textarea");
            textArea.Setup(m => m.GetAttribute("name")).Returns(TextAreaName);
            return textArea;
        }

        public Mock<IWebElement> TextInput
        { get { return textInput; } }

        public Mock<IWebElement> CheckBox
        { get { return checkBox; } }

        public Mock<IWebElement> SelectOption
        { get { return selectOption; } }

        public Mock<IWebElement> RadioButton
        { get { return radioButton; } }

        public Mock<IWebElement> TextArea
        { get { return textArea; } }
    }
}
