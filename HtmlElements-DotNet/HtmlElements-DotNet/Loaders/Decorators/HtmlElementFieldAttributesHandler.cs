using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Exceptions;
using Yandex.HtmlElements.PageFactories;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Loaders.Decorators
{
    // Done
    public class HtmlElementFieldAttribytesHandler : DefaultFieldAttributesHandler
    {
        public HtmlElementFieldAttribytesHandler(FieldInfo field)
            : base(field)
        { }

        public override By BuildBy()
        {
            if (HtmlElementUtils.IsHtmlElement(Field) || HtmlElementUtils.IsHtmlElementList(Field))
            {
                Type type = HtmlElementUtils.IsHtmlElementList(Field) ? HtmlElementUtils.GetGenericParameterType(Field) : Field.FieldType;
                return BuildByFromHtmlElementAttributes(type);
            }
            else
            {
                return base.BuildBy();
            }
        }

        private By BuildByFromHtmlElementAttributes(Type type)
        {
            FindsByAttribute[] findBys = (FindsByAttribute[])Field.GetCustomAttributes(typeof(FindsByAttribute));
            if (findBys.Length > 0)
            {
                return BuildByFromFindsBys(findBys);
            }

            BlockAttribute[] blocks = (BlockAttribute[])type.GetCustomAttributes(typeof(BlockAttribute), true);
            if (blocks.Length > 0)
            {
                BlockAttribute block = blocks[0];
                FindsByAttribute findsBy = block.Value;
                return BuildByFromFindsBy(findsBy);
            }
            return BuildByFromDefault();
        }
    }
}
