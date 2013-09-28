using OpenQA.Selenium;
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
    public class SearchArrow : HtmlElement
    {
        [Name(SearchArrowData.RequestInputName)]
        [FindsBy(How = How.ClassName, Using = SearchArrowData.RequestInputClass)]
        private TextInput requestInput;

        [FindsBy(How = How.ClassName, Using = SearchArrowData.SearchButtonClass)]
        private Button searchButton;

        [FindsBy(How = How.ClassName, Using = SearchArrowData.SuggestClass)]
        private List<IWebElement> suggestList;

        public TextInput RequestInput
        {
            get { return requestInput; }
        }

        public Button SearchButton
        {
            get { return searchButton; }
        }

        public List<IWebElement> SuggestList
        {
            get { return suggestList; }
        }
    }
}
