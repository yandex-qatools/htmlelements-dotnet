using System;
using Common.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Dynamic;
using OpenQA.Selenium.Internal;
using System.Drawing;
using OpenQA.Selenium.Interactions.Internal;
using System.Collections.ObjectModel;

namespace HtmlElements.Test.Tests
{

    public class WebElementNamedProxy : IWebElement, IWrapsElement, ILocatable
    {
        private dynamic handler;

        public WebElementNamedProxy(DynamicObject handler)
        {
            this.handler = handler;
        }

        public void Clear()
        {
            handler.Clear();
        }

        public void Click()
        {
            handler.Click();
        }

        public bool Displayed
        {
            get { return handler.Displayed; }
        }

        public bool Enabled
        {
            get { return handler.Enabled; }
        }

        public string GetAttribute(string attributeName)
        {
            return handler.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return handler.GetCssValue(propertyName);
        }

        public Point Location
        {
            get { return handler.Location; }
        }

        public bool Selected
        {
            get { return handler.Selected; }
        }

        public void SendKeys(string text)
        {
            handler.SendKeys(text);
        }

        public Size Size
        {
            get { return handler.Size; }
        }

        public void Submit()
        {
            handler.Submit();
        }

        public string TagName
        {
            get { return handler.TagName; }
        }

        public string Text
        {
            get { return handler.Text; }
        }

        public IWebElement FindElement(By by)
        {
            return handler.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return handler.FindElements(by);
        }

        public IWebElement WrappedElement
        {
            get { return handler.WrappedElement; }
        }

        public ICoordinates Coordinates
        {
            get { return handler.Coordinates; }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get { return handler.LocationOnScreenOnceScrolledIntoView; }
        }

        public override string ToString()
        {
            return handler.ToString();
        }
    }

    public class WebElementListNamedProxyHandler : DynamicObject
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(WebElementListNamedProxyHandler));

        public WebElementListNamedProxyHandler()
            : base()
        {
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if ("ToString" == binder.Name)
            {
                Log.Info("In ToString");
                result = "ToString called";
                return true;
            }
            result = null;

            if ("Click" == binder.Name)
            {
                Log.Info("In Click");
                return true;
            }

            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if ("WrappedElement" == binder.Name)
            {
                Log.Info("In WrappedElement");
                return true;
            }
            return false;
        }
    }

    [TestFixture]
    public class TestingTest : BasicTest
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(TestingTest));

        //[Test]
        public void TestDynamicObject()
        {
            IWebElement element = new WebElementNamedProxy(new WebElementListNamedProxyHandler());
            Log.Info(element.ToString());
            element.Click();
            Log.Info(((IWrapsElement)element).WrappedElement);
        }

        //[Test]
        public void TestMethod1()
        {
            Uri uri = new Uri("assembly://Selenium-Test/Selenium.Test/AppContext.xml");
            Log.Info(Uri.SchemeDelimiter);
            Log.Info(uri.GetType().Name);
        }

        [Test]
        public void TestTypes()
        {
            
        }
    }
}
