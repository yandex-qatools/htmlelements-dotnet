using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using HtmlElements.Test.Browsers;
using HtmlElements.Test.Screens.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElements.Test.Screens
{
    public abstract class BasicScreen : IScreen
    {
        protected Browser browser;
        protected By[] identities;

        public BasicScreen(Browser browser)
        {
            this.browser = browser;
        }

        protected void Init()
        {
            browser.Init(this);
        }

        public abstract void Open();

        public Browser Browser
        {
            get
            {
                return browser;
            }
        }

        public By[] Identities
        {
            get
            {
                if (identities != null && identities.Length > 0)
                {
                    return identities;
                }
                Type type = GetType();
                IdentityAttribute[] arttributes = (IdentityAttribute[])type.GetCustomAttributes(typeof(IdentityAttribute), false);
                if (arttributes.Length <= 0)
                {
                    throw new NoScreenIdentityException("Screen Idntity wasn't found for Screen type: " + type.ToString());
                }
                return arttributes[0].Identities;
            }
            set
            {
                identities = value;
            }
        }

        public bool IsOnCurrentPage()
        {
            return browser.IsOnScreen(this);
        }
    }
}
