using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yandex.HtmlElements.PageFactories
{
    // Done
    public abstract class AttributesHandler
    {
        public abstract By BuildBy();

        public abstract bool ShouldCache();

        protected virtual By BuildByFromFindsBys(FindsByAttribute[] findBys)
        {
            AssertValidFindBys(findBys);

            By[] byArray = new By[findBys.Length];
            for (int i = 0; i < findBys.Length; i++)
            {
                byArray[i] = BuildByFromFindsBy(findBys[i]);
            }

            return new ByChained(byArray);
        }

        protected virtual By BuildByFromFindsBy(FindsByAttribute findsBy)
        {
            AssertValidFindBy(findsBy);
            return BuildBy(findsBy);
        }

        protected virtual By BuildBy(FindsByAttribute findsBy)
        {
            How how = findsBy.How;
            string usingStr = findsBy.Using;
            Type customType = findsBy.CustomFinderType;

            switch (how)
            {
                case How.Id:
                    return By.Id(usingStr);
                case How.Name:
                    return By.Name(usingStr);
                case How.TagName:
                    return By.TagName(usingStr);
                case How.ClassName:
                    return By.ClassName(usingStr);
                case How.CssSelector:
                    return By.CssSelector(usingStr);
                case How.LinkText:
                    return By.LinkText(usingStr);
                case How.PartialLinkText:
                    return By.PartialLinkText(usingStr);
                case How.XPath:
                    return By.XPath(usingStr);
                case How.Custom:
                    ConstructorInfo ctor = customType.GetConstructor(new Type[] { typeof(string) });
                    By finder = ctor.Invoke(new object[] { usingStr }) as By;
                    return finder;
                default:
                    throw new ArgumentException(string.Format("Did not know how to construct How from how {0}, using {1}", how, usingStr));
            }
        }

        private void AssertValidFindBys(FindsByAttribute[] findBys)
        {
            foreach (FindsByAttribute findBy in findBys)
            {
                AssertValidFindBy(findBy);
            }
        }

        private void AssertValidFindBy(FindsByAttribute findBy)
        {
            if (findBy.How == How.Custom)
            {
                if (findBy.CustomFinderType == null)
                {
                    throw new ArgumentException(
                            "If you set the 'How' property to 'Custom' value, you must also set 'CustomFinderType'");
                }
                if (!findBy.CustomFinderType.IsSubclassOf(typeof(By)))
                {
                    throw new ArgumentException("Custom finder type must be a descendent of the 'By' class");
                }

                ConstructorInfo ctor = findBy.CustomFinderType.GetConstructor(new Type[] { typeof(string) });
                if (ctor == null)
                {
                    throw new ArgumentException("Custom finder type must expose a public constructor with a string argument");
                }
            }
        }
    }
}
