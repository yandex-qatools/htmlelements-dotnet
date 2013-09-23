using System;
using Common.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Dynamic;
using OpenQA.Selenium.Internal;
using System.Drawing;
using OpenQA.Selenium.Interactions.Internal;
using System.Collections.ObjectModel;
using System.IO;

namespace HtmlElements.Test.Tests
{
    [TestFixture]
    public class TestingTest : BasicTest
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(TestingTest));

        [Test]
        public void TestPath()
        {
            Log.Info(System.IO.Directory.GetCurrentDirectory());
            Log.Info(Environment.CurrentDirectory);
            Log.Info(Path.GetFullPath(@"."));

            string fullPath = Path.Combine(@"d:\archives", @"..\media");
            fullPath = Path.GetFullPath(fullPath);
            Log.Info(fullPath);
        }

        //[Test]
        public void TestMethod1()
        {
            Uri uri = new Uri("assembly://Selenium-Test/Selenium.Test/AppContext.xml");
            Log.Info(Uri.SchemeDelimiter);
            Log.Info(uri.GetType().Name);
        }
    }
}
