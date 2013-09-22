using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Factories;

namespace HtmlElements.Test.Screens
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IdentityAttribute : Attribute
    {
        public const char _IdentityDelimeter = ':';

        private By[] byIdentities;

        public IdentityAttribute(params string[] identityStrings)
        {
            byIdentities = new By[identityStrings.Length];
            for (int i = 0; i < identityStrings.Length; i++ )
            {
                string identity = identityStrings[i];
                int delimiterPos = identity.IndexOf(_IdentityDelimeter);
                String howStr = identity.Substring(0, delimiterPos);
                String usingStr = identity.Substring(delimiterPos + 1).TrimStart();
                How how;
                if (Enum.TryParse(howStr, true, out how))
                {
                    byIdentities[i] = ByFactory.From(how, usingStr);
                }
            }
        }

        public By[] Identities { get { return byIdentities; } set { byIdentities = value; } }
    }
}
