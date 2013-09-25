using System;
using Common.Logging;
using Spring.Testing.NUnit;
using HtmlElements.Test.Services;
using System.Configuration;
using HtmlElements.Test.Screens;
using HtmlElements.Test.Browsers;

namespace HtmlElements.Test.Tests
{
    public class BasicTest : AbstractDependencyInjectionSpringContextTests
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(BasicTest));

        protected override string[] ConfigLocations
        {
            get
            {
                return new String[] { ConfigurationManager.AppSettings["Spring.Context.Location"] };
            }
        }

        protected override void OnTearDown()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["Spring.Context.Dispose"]))
            {
                base.OnTearDown();
                applicationContext.Dispose();
            }
        }
    }

    public class SeleniumTest : BasicTest
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(SeleniumTest));

        protected GoogleGuiService googleService;

        public GoogleGuiService GoogleService
        {
            set
            {
                googleService = value;
            }
        }

        private BrowserFactory browserFactory;

        public BrowserFactory BrowserFactory
        {
            set { browserFactory = value; }
        }

        public Browser Browser
        {
            get
            {
                return browserFactory.Get();
            }
        }
    }
}