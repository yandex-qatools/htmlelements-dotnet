using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.PageFactories.Selenium
{
    public interface IFieldDecorator
    {
        object Decorate(FieldInfo field);
    }
}
