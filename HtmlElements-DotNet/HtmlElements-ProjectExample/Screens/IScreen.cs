using OpenQA.Selenium;
using HtmlElements.Test.Browsers;
using System;

namespace HtmlElements.Test.Screens
{
    public interface IScreen
    {
        void Open();

        Browser Browser
        {
            get;
        }

        By[] Identities
        {
            get;
        }
    }

    public interface IMainScreen : IScreen
    {
        Uri BaseUrl
        {
            get;
        }
    }
}
