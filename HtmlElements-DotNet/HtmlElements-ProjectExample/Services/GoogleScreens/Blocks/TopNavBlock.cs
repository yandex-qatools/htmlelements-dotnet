using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Elements;

namespace HtmlElements.Test.Services.GoogleScreens.Blocks
{
    [Name("Top Navigation Block")]
    [Block(How = How.XPath, Using = "//div[@id='gbz']/ol")]
    public class TopNavBlock : HtmlElement
    {
        [Name("Navigation Links")]
        [FindsBy(How = How.XPath, Using = "li/a[contains(@id,'gb')]")]
        IList<Link> navLinks;

        [Name("Images Link")]
        [FindsBy(How = How.XPath, Using = "li/a[contains(@id,'gb_2')]")]
        Link imagesLink;

        public void Navigate(string navLinkPart)
        {
            foreach (Link link in navLinks)
            {
                if (link.GetReference().Contains(navLinkPart))
                {
                    link.Click();
                    break;
                }
            }
        }
    }
}
