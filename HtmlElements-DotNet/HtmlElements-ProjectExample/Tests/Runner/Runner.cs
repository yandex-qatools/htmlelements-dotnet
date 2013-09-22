using Common.Logging;
using NUnit.Core;
using System;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml;

namespace HtmlElements.Test.Tests.Runner
{
    public class ClassTestFilter : TestFilter
    {
        private IList<string> testClasses;

        public ClassTestFilter(IList<string> testClasses)
        {
            this.testClasses = testClasses;
        }


        public override bool Match(ITest test)
        {
            foreach (string testClass in testClasses)
            {
                if (testClass.Equals(test.ClassName))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class RunnerListener : EventListener
    {
        private static readonly ILog _log = LogManager.GetLogger("Test Listener");

        public void RunFinished(Exception exception)
        {
            _log.Error(exception.Message, exception);
        }

        public void RunFinished(TestResult result)
        {
            _log.Error("Run finished.");
        }

        public void RunStarted(string name, int testCount)
        {
            _log.Info(String.Format("Run started. Name: {0}; Test count: {1}", name, testCount));
        }

        public void SuiteFinished(TestResult result)
        {
            _log.Debug("Suite finished.");
        }

        public void SuiteStarted(TestName testName)
        {
            _log.Debug("Suite started: " + testName);
        }

        public void TestFinished(TestResult result)
        {
            ResultState state = result.ResultState;
            _log.Info(string.Format("Test finished. ID: {0}; Assertions: {1}; Result: {2}", result.Test.TestName.ToString(), result.AssertCount, state.ToString()));
            switch (state)
            {
                case ResultState.Success:
                case ResultState.Inconclusive:
                    break;
                default:
                    _log.Info("Reason: " + result.Message);
                    _log.Info("Stack trace: " + result.StackTrace);
                    break;
            }
        }

        public void TestOutput(TestOutput testOutput)
        {
            _log.Info("Test output: " + testOutput.ToString());
        }

        public void TestStarted(TestName testName)
        {
            _log.Info("Test started: " + testName);
        }

        public void UnhandledException(Exception exception)
        {
            _log.Error(exception.Message, exception);
        }
    }

    public class Runner
    {
        public const string _TestClassExtractionXpath = "/nr:suite/nr:test/@class";
        public const string _TestSuiteExtractionXpath = "/nr:suite/@name";
        public const string _TestNamespace = "http://hardnorth.net/nunit-runner/XMLSchema.xsd";
        public const string _TestNamespacePrefix = "nr";

        private static readonly ILog _log = LogManager.GetLogger(typeof(Runner));

        public static void Main()
        {
            // Set common application locale, check 'app.config' for this property
            SetLocale(ConfigurationManager.AppSettings["Locale"]);

            // Get test data from scenario file
            Scenario scenario = GetScenario(ConfigurationManager.AppSettings["Nunit.Runner.Scenario"]);
            string suite = scenario.Name;
            IList<string> testClasses = scenario.Tests;

            // Start tests
            CoreExtensions.Host.InitializeService();
            SimpleTestRunner runner = new SimpleTestRunner();
            TestPackage package = new TestPackage(suite);
            string loc = Assembly.GetExecutingAssembly().Location;
            package.Assemblies.Add(loc);
            try
            {
                if (runner.Load(package))
                {
                    TestResult result = runner.Run(new RunnerListener(), new ClassTestFilter(testClasses), true, LoggingThreshold.Debug);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private static Scenario GetScenario(string path)
        {
            XmlDocument from = new XmlDocument();
            from.Load(path);
            return new Scenario(GetSuiteName(from), GetTestClasses(from));
        }

        private static string GetSuiteName(XmlDocument from)
        {
            IList<string> result = GetByXPath(from, _TestSuiteExtractionXpath);
            if (result.Count <= 0)
            {
                throw new IllegalSuteException("Couldn't locate sute name, check sute file location and syntax.");
            }
            return GetByXPath(from, _TestSuiteExtractionXpath)[0];
        }

        private static IList<string> GetTestClasses(XmlDocument from)
        {
            return GetByXPath(from, _TestClassExtractionXpath);
        }

        private static IList<string> GetByXPath(XmlDocument from, string xpath)
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(from.NameTable);
            namespaces.AddNamespace(_TestNamespacePrefix, _TestNamespace);
            XmlNodeList list = from.SelectNodes(xpath, namespaces);

            IList<string> result = new List<string>();
            for (int i = 0; i < list.Count; i++ )
            {
                result.Add(list.Item(i).Value);
            }
            return result;
        }

        private static void SetLocale(string locale)
        {
            CultureInfo myCulture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = myCulture;
            CultureInfo.DefaultThreadCurrentCulture = myCulture;
            CultureInfo.DefaultThreadCurrentUICulture = myCulture;
        }
    }

    public class Scenario
    {
        string name;
        IList<string> tests;

        public Scenario(string name, IList<string> tests)
        {
            this.name = name;
            this.tests = tests;
        }

        public string Name
        {
            get { return name; }
        }

        public IList<string> Tests
        {
            get { return tests; }
        }
    }
}
