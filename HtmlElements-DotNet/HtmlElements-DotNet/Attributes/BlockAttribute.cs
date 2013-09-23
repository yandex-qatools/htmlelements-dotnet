using OpenQA.Selenium.Support.PageObjects;
using System;
using System.ComponentModel;

namespace Yandex.HtmlElements.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class BlockAttribute : Attribute
    {
        [DefaultValue(How.Id)]
        public How How { get; set; }

        public string Using { get; set; }

        [DefaultValue(0)]
        public int Priority { get; set; }

        public Type CustomFinderType { get; set; }

        public FindsByAttribute Value
        {
            get
            {
                FindsByAttribute result = new FindsByAttribute();
                result.How = How;
                result.Using = Using;
                result.Priority = Priority;
                result.CustomFinderType = CustomFinderType;
                return result;
            }
        }
    }
}
