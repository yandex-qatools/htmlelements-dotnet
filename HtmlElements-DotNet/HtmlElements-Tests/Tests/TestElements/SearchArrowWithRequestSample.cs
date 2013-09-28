using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Elements;

namespace HtmlElements.Tests.TestElements
{
    [Name(SearchArrowData.SearchArrowName)]
    [Block(How = How.ClassName, Using = SearchArrowData.SearchArrowClass)]
    public class SearchArrowWithRequestSample : SearchArrow
    {
        [FindsBy(How = How.ClassName, Using = SearchArrowData.SearchRequestSampleClass)]
        private Link searchRequestSample;

        public Link GetSearchRequestSample()
        {
            return searchRequestSample;
        }
    }
}
