using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yandex.HtmlElements.Elements;
using OpenQA.Selenium;
using Moq;
using HtmlElements.Tests.TestElements;
using System.Collections.Generic;

namespace HtmlElements.Tests
{
    [TestClass]
    public class FormFillingTest
    {
        private const String InputTextToSend = "text";
        private const bool CheckboxValueToSet = true;

        private Mock<IWebElement> mockedElement;

        private MockedForm form;

        [TestInitialize]
        public void fillForm()
        {
            mockedElement = new Mock<IWebElement>();
            form = new MockedForm(mockedElement);

            // Prepare data to fill form with
            IDictionary<string, object> data = new Dictionary<string, object>();
            data[MockedForm.TextInputName] = InputTextToSend;
            data[MockedForm.CheckboxName] = CheckboxValueToSet;
            data[MockedForm.RadioName] = MockedForm.RadioButtonValue;
            data[MockedForm.SelectName] = MockedForm.SelectOptionValue;
            data[MockedForm.TextAreaName] = InputTextToSend;

            // Fill form
            form.Fill(data);
        }

        [TestMethod]
        public void FormFieldsShouldBeFilledCorrectly()
        {
            form.TextInput.Verify(m => m.SendKeys(InputTextToSend));
            form.CheckBox.Verify(m => m.Click());
            form.RadioButton.Verify(m => m.Click());
            form.SelectOption.Verify(m => m.Click());
            form.TextArea.Verify(m => m.SendKeys(InputTextToSend));
        }
    }
}
