using System;

namespace HtmlElements.Test.Screens.Exceptions
{
    public class NoRouteToScreenException : Exception
    {
        public NoRouteToScreenException()
            : base()
        {
        }

        public NoRouteToScreenException(string message)
            : base(message)
        {
        }

        public NoRouteToScreenException(string message, Exception isserException)
            : base(message, isserException)
        {
        }
    }
}
