using HtmlElements.Test.Factories.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace HtmlElements.Test.Factories
{
    // Just a cutted copy of WebDriver's ByFactory
    public static class ByFactory
    {
        public static By From(How how, string usingStr)
        {
            switch (how)
            {
                case How.Id:
                    return By.Id(usingStr);
                case How.Name:
                    return By.Name(usingStr);
                case How.TagName:
                    return By.TagName(usingStr);
                case How.ClassName:
                    return By.ClassName(usingStr);
                case How.CssSelector:
                    return By.CssSelector(usingStr);
                case How.LinkText:
                    return By.LinkText(usingStr);
                case How.PartialLinkText:
                    return By.PartialLinkText(usingStr);
                case How.XPath:
                    return By.XPath(usingStr);
                default:
                    throw new UnsupportedIdentityTypeException(string.Format("Unable to locate an Identity using locateor type: {0}", how));
            }
        }
    }
}

