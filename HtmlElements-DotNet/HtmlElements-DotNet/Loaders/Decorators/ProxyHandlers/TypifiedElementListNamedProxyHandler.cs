using ImpromptuInterface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.PageFactories.Selenium;

namespace Yandex.HtmlElements.Loaders.Decorators.ProxyHandlers
{
    public class TypifiedElementListNamedProxyHandler : DynamicObject
    {
        private Type elementType;
        private IElementLocator locator;
        private string name;

        private TypifiedElementListNamedProxyHandler(Type elementType, IElementLocator locator, string name)
            : base()
        {
            this.elementType = elementType;
            this.locator = locator;
            this.name = name;
        }

        public static IList<TypifiedElement> newInstance(Type listType, Type elementType, IElementLocator locator, string name)
        {
            return new TypifiedElementListNamedProxyHandler(elementType, locator, name).ActLike(listType);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            IList<TypifiedElement> elements = GetElements();

            try
            {
                result = elements.GetType().GetMethod(binder.Name).Invoke(elements, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            IList<TypifiedElement> elements = GetElements();

            try
            {
                result = elements.GetType().GetProperty(binder.Name).GetValue(elements);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int index = (int)indexes[0];
            IList<TypifiedElement> elements = GetElements();
            try
            {
                result = elements[index];
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        private IList<TypifiedElement> GetElements()
        {
            IList<TypifiedElement> typifiedElements = new List<TypifiedElement>();
            IList<IWebElement> elements = locator.FindElements();
            int elementNumber = 0;
            foreach (IWebElement element in elements)
            {
                TypifiedElement typifiedElement = HtmlElementFactory.CreateTypifiedElementInstance(elementType, element);
                string typifiedElementName = string.Format("{0} {1}", name, elementNumber);
                typifiedElement.Name = typifiedElementName;
                typifiedElements.Add(typifiedElement);
                elementNumber++;
            }
            return typifiedElements;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
