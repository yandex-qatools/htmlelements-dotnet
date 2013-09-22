using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.PageFactories
{
    //Done
    public class DefaultFieldAttributesHandler : AttributesHandler
    {
        private FieldInfo field;

        public FieldInfo Field
        {
            get { return field; }
        }

        public DefaultFieldAttributesHandler(FieldInfo field)
        {
            this.field = field;
        }

        public override bool ShouldCache()
        {
            return field.GetCustomAttribute<CacheLookupAttribute>(false) != null;
        }

        public override By BuildBy()
        {
            By ans = null;

            FindsByAttribute[] findBys = (FindsByAttribute[])field.GetCustomAttributes(typeof(FindsByAttribute), false);
            if (findBys.Length > 0)
            {
                ans = BuildByFromFindsBys(findBys.ToArray<FindsByAttribute>());
            }

            if (ans == null)
            {
                ans = BuildByFromDefault();
            }

            if (ans == null)
            {
                throw new ArgumentException("Cannot determine how to locate element " + field);
            }

            return ans;
        }

        protected virtual By BuildByFromDefault()
        {
            return new ByChained(By.Id(field.Name), By.Name(field.Name));
        }
    }
}
