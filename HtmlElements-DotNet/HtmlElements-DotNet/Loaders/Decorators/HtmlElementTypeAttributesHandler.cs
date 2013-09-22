using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Exceptions;
using Yandex.HtmlElements.PageFactories;

namespace Yandex.HtmlElements.Loaders.Decorators
{
    public class HtmlElementTypeAttributesHandler : AttributesHandler
    {
        private readonly Type htmlElementType;

        public HtmlElementTypeAttributesHandler(Type type)
        {
            this.htmlElementType = type;
        }

        public override By BuildBy()
        {
            BlockAttribute[] blocks = (BlockAttribute[])htmlElementType.GetCustomAttributes(typeof(BlockAttribute), true);
            if (blocks.Length > 0)
            {
                BlockAttribute block = blocks[0];
                FindsByAttribute findsBy = block.Value;
                return BuildByFromFindsBy(findsBy);
            }
            throw new HtmlElementsException(string.Format("Cannot determine how to locate instance of {0}",
                    htmlElementType));
        }

        public override bool ShouldCache()
        {
            return false;
        }
    }
}
