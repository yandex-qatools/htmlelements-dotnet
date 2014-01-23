using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using System;
using System.Threading;

namespace HtmlElements.Test.Browsers
{
    public class BrowserFactory : IDisposable
    {
        private ThreadLocal<Browser> browserPool;
        private BrowserType browserType;
        private int timeoutSeconds = 10;
        private int pollingInterval = 1000;
        private string profilePath;

        public BrowserFactory(BrowserType browserType)
        {
            this.browserType = browserType;
            Func<Browser> factory = this.GetBrowser;
            browserPool = new ThreadLocal<Browser>(factory, true);
        }

        public Browser Get()
        {
            Browser result = browserPool.Value;
            return result;
        }

        private Browser GetBrowser()
        {
            Browser result = new Browser(GetDriver());
            result.Timeout = timeoutSeconds;
            result.PollingInterval = pollingInterval;
            return result;
        }

        private IWebDriver GetDriver()
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Firefox:
                    if (profilePath == null)
                    {
                        return new FirefoxDriver(new FirefoxProfile());
                    }
                    else
                    {
                        return new FirefoxDriver(new FirefoxProfile(profilePath));
                    }
                case BrowserType.IE:
                    return new InternetExplorerDriver();
                case BrowserType.PhantomJs:
                    return new PhantomJSDriver();
                case BrowserType.Safari:
                    return new SafariDriver();
                default:
                    return new FirefoxDriver();
            }
        }

        public void Dispose()
        {
            foreach (Browser browser in browserPool.Values)
            {
                browser.Dispose();
            }
            browserPool.Dispose();
        }

        public int LoadTimeout
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

        public string ProfilePath
        {
            get
            {
                return profilePath;
            }
            set
            {
                profilePath = value;
            }
        }
    }
}
