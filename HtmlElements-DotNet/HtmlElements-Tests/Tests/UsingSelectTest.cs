using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Moq;
using Yandex.HtmlElements.Attributes;
using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Loaders;
using System.Collections.Generic;

namespace HtmlElements.Tests
{
    [TestClass]
    public class UsingSelectTest
    {
        [TestMethod]
        public void WithSimpleElement()
        {
            Mock<IWebDriver> driver = new Mock<IWebDriver>();
            Mock<IWebElement> registerForm = new Mock<IWebElement>();
            Mock<IWebElement> countryBoxElement = new Mock<IWebElement>();

            driver.Setup(drv => drv.FindElement(By.ClassName("regform"))).Returns(registerForm.Object);
            registerForm.Setup(rf => rf.FindElement(new ByChained(By.Name("country")))).Returns(countryBoxElement.Object);
            countryBoxElement.Setup(cbe => cbe.FindElements(By.TagName("option"))).Returns(new List<IWebElement>().AsReadOnly());
            countryBoxElement.Setup(cbe => cbe.TagName).Returns("select");
            countryBoxElement.Setup(cbe => cbe.GetAttribute("multiple")).Returns("true");

            RegisterForm form = new RegisterForm(driver.Object);
            form.country.DeselectAll();
        }
    }

    [Block(How = How.ClassName, Using = "regform")]
    public class RegisterForm : HtmlElement
    {
        public RegisterForm(IWebDriver driver)
        {
            HtmlElementLoader.Populate(this, driver);
        }

        [FindsBy(How = How.Name, Using = "country")]
        public Select country;
    }
}
