using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Loaders.Decorators;

namespace Yandex.HtmlElements.PageFactories.Selenium
{
    public class PageFactory
    {
        public static void InitElements(IFieldDecorator decorator, object page)
        {
            Type proxyIn = page.GetType();
            while (proxyIn != typeof(object))
            {
                ProxyFields(decorator, page, proxyIn);
                proxyIn = proxyIn.BaseType;
            }
        }

        private static void ProxyFields(IFieldDecorator decorator, object page, Type proxyIn)
        {
            FieldInfo[] fields = proxyIn.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (FieldInfo field in fields)
            {
                object value = decorator.Decorate(field);
                if (value != null)
                {
                    field.SetValue(page, value);
                }
            }
        }
    }
}
