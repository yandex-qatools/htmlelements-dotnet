using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.PageFactories
{

    public delegate IWebElement FindElement();
    public delegate bool IsElementUsable(IWebElement element);

    public class AjaxElementLocator : DefaultElementLocator
    {
        private const int DefaultTimeout = 5;

        private IClock clock;
        private int timeOutInSeconds;

        private TimeSpan sleepInterval = TimeSpan.FromMilliseconds(250);

        public AjaxElementLocator(ISearchContext searchContext, AttributesHandler attributesHandler)
            : this(searchContext, DefaultTimeout, attributesHandler)
        { }

        public AjaxElementLocator(ISearchContext searchContext, int timeOutInSeconds, AttributesHandler attributesHandler)
            : this(new SystemClock(), searchContext, timeOutInSeconds, attributesHandler)
        { }

        public AjaxElementLocator(IClock clock, ISearchContext searchContext, int timeOutInSeconds, AttributesHandler attributesHandler)
            : base(searchContext, attributesHandler)
        {
            this.clock = clock;
            this.timeOutInSeconds = timeOutInSeconds;
        }

        public override IWebElement FindElement()
        {
            SlowLoadingElement loadingElement = new SlowLoadingElement(clock, timeOutInSeconds, SleepInterval, base.FindElement, this.IsElementUsable);
            try
            {
                return loadingElement.Load().Element;
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException(
                        string.Format("Timed out after {0} seconds. {1}", timeOutInSeconds, e.Message),
                        e.InnerException);
            }
        }

        protected virtual TimeSpan SleepInterval
        {
            get { return sleepInterval; }
        }

        protected virtual bool IsElementUsable(IWebElement element)
        {
            return true;
        }


        private class SlowLoadingElement : SlowLoadableComponent<SlowLoadingElement>
        {

            private NoSuchElementException lastException;
            private IWebElement element;
            private FindElement elementFindingFunc;
            private IsElementUsable elementIsUsableFunc;


            public SlowLoadingElement(IClock clock, int timeOutInSeconds, TimeSpan sleepInterval, FindElement elementFindingFunc, IsElementUsable elementIsUsableFunc)
                : base(TimeSpan.FromSeconds(timeOutInSeconds), clock)
            {
                this.elementFindingFunc = elementFindingFunc;
                this.elementIsUsableFunc = elementIsUsableFunc;
                SleepInterval = sleepInterval;
            }

            protected override void ExecuteLoad()
            {
                // Does nothing
            }

            protected override bool EvaluateLoadedStatus()
            {
                try
                {
                    element = elementFindingFunc();
                    if (!elementIsUsableFunc(element))
                    {
                        throw new NoSuchElementException("Element is not usable");
                    }
                    return true;
                }
                catch (NoSuchElementException e)
                {
                    lastException = e;
                    // Should use JUnit's AssertionError, but it may not be present
                    throw new NoSuchElementException("Unable to locate the element", e);
                }
            }

            public NoSuchElementException LastException
            {
                get { return lastException; }
            }

            public IWebElement Element
            {
                get { return element; }
            }
        }
    }
}
