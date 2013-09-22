using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.HtmlElements.Exceptions
{
    public class HtmlElementsException : Exception
    {
        public HtmlElementsException()
            : base()
        {
        }

        public HtmlElementsException(string message)
            : base(message)
        {
        }

        public HtmlElementsException(string message, Exception isserException)
            : base(message, isserException)
        {
        }
    }
}
