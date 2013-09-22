using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlElements.Test.Tests.Runner
{
    class IllegalSuteException : Exception
    {
        public IllegalSuteException(string message)
            : base(message)
        {
        }
    }
}
