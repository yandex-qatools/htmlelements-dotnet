using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using HtmlElements.Test.Screens;
using System;
using Yandex.HtmlElements.Loaders.Decorators;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace HtmlElements.Test.Browsers
{
    public class Browser : IDisposable
    {
        private IWebDriver driver;

        private int timeoutSeconds;

        private int pollingInterval;

        private IScreen currentScreen;

        public Browser(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Init(IScreen pageObject)
        {
            WebDriverWait waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            waiter.PollingInterval = TimeSpan.FromMilliseconds(pollingInterval);
            waiter.IgnoreExceptionTypes(typeof(OpenQA.Selenium.NoSuchElementException));
            foreach (By identity in pageObject.Identities)
            {
                Func<IWebDriver, IWebElement> condition = delegate(IWebDriver drv) { return drv.FindElement(identity); };
                IWebElement element = waiter.Until(condition);
            }
            PageFactory.InitElements(new HtmlElementDecorator(driver), pageObject);
            currentScreen = pageObject;
        }

        public void GetPage(IMainScreen pageObject)
        {
            driver.Navigate().GoToUrl(pageObject.BaseUrl);
            Init(pageObject);
        }

        public void Refresh()
        {
            driver.Navigate().Refresh();
        }

        public int Timeout
        {
            get
            {
                return timeoutSeconds;
            }
            set
            {
                timeoutSeconds = value;
            }
        }

        public int PollingInterval
        {
            get
            {
                return pollingInterval;
            }
            set
            {
                pollingInterval = value;
            }
        }

        internal bool IsOnScreen(IScreen screen)
        {
            foreach (By identity in screen.Identities)
            {
                try
                {
                    driver.FindElement(identity);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public IScreen CurrentScreen
        {
            get { return currentScreen; }
        }

        public void Dispose()
        {
            driver.Dispose();
            currentScreen = null;
        }
    }
}
