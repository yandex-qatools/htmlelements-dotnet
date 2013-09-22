using System;

namespace HtmlElements.Test.Screens.Exceptions
{
    public class NoScreenIdentityException : Exception
    {
        public NoScreenIdentityException()
            : base()
        {
        }

        public NoScreenIdentityException(string message)
            : base(message)
        {
        }

        public NoScreenIdentityException(string message, Exception isserException)
            : base(message, isserException)
        {
        }
    }
}
