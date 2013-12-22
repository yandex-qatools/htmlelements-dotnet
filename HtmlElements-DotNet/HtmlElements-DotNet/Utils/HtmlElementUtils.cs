using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Yandex.HtmlElements.Attributes;
using Yandex.HtmlElements.Elements;

namespace Yandex.HtmlElements.Utils
{
    public static class HtmlElementUtils
    {
        private const char PathDelimeter = '/';

        private const char NamespaceDelimeter = '.';

        public static T newInstance<T>(Type type, params object[] args)
        {
            Type resultType = typeof(T);
            if (type.IsClass && resultType.IsAssignableFrom(type))
            {
                return (T)Activator.CreateInstance(type, args);
            }
            return default(T);
        }

        public static bool IsOnRemoteWebDriver(IWebElement element)
        {
            return IsRemoteWebElement(element);
        }

        public static bool IsRemoteWebElement(IWebElement element)
        {
            return element.GetType() == typeof(RemoteWebElement);
        }

        public static bool IsExistInAssembly(string fileName)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            ManifestResourceInfo info = assembly.GetManifestResourceInfo(fileName);
            return info != null;
        }

        public static string ExtractResource(Uri uri)
        {
            string hostAssembly = uri.Host;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == hostAssembly)
                {
                    string nameAndNamespacePath = uri.AbsolutePath;
                    string namespaceStr = nameAndNamespacePath.Substring(0, nameAndNamespacePath.IndexOf(PathDelimeter));
                    string nameStr = nameAndNamespacePath.Substring(nameAndNamespacePath.IndexOf(PathDelimeter) + 1, nameAndNamespacePath.Length);
                    string nameAndNamespace = namespaceStr + NamespaceDelimeter + nameStr;

                    Stream resource = assembly.GetManifestResourceStream(nameAndNamespace);
                    if (resource != null)
                    {
                        string tempFilePath = Path.GetTempPath() + Path.PathSeparator + Path.GetRandomFileName() + Path.PathSeparator + nameStr;
                        CopyResource(resource, tempFilePath);
                        return tempFilePath;
                    }
                }
            }
            return null;
        }

        private static void CopyResource(Stream from, string to)
        {
            using (from)
            {
                using (FileStream fileStream = new FileStream(to, FileMode.Create))
                {
                    for (int i = 0; i < from.Length; i++)
                    {
                        fileStream.WriteByte((byte)from.ReadByte());
                    }
                }
            }
        }

        public static bool IsWebElement(FieldInfo field)
        {
            return IsWebElement(field.FieldType);
        }

        public static bool IsWebElement(Type type)
        {
            return typeof(IWebElement).IsAssignableFrom(type);
        }

        public static bool IsWebElementList(FieldInfo field)
        {
            return IsWebElementList(field.FieldType);
        }

        public static bool IsWebElementList(Type type)
        {
            if (!IsParametrizedGenericList(type))
            {
                return false;
            }
            Type collectionParameterType = GetGenericParameterType(type);
            return IsWebElement(collectionParameterType);
        }

        public static bool IsTypifiedElement(FieldInfo field)
        {
            return IsTypifiedElement(field.FieldType);
        }

        private static bool IsTypifiedElement(Type type)
        {
            return typeof(TypifiedElement).IsAssignableFrom(type);
        }

        public static bool IsTypifiedElementList(FieldInfo field)
        {
            return IsTypifiedElementList(field.FieldType);
        }

        private static bool IsTypifiedElementList(Type type)
        {
            if (!IsParametrizedGenericList(type))
            {
                return false;
            }
            Type collectionParameterType = GetGenericParameterType(type);
            return IsTypifiedElement(collectionParameterType);
        }

        public static bool IsHtmlElement(FieldInfo field)
        {
            return IsHtmlElement(field.FieldType);
        }

        public static bool IsHtmlElement(Type type)
        {
            return typeof(HtmlElement).IsAssignableFrom(type);
        }

        public static bool IsHtmlElement(object instance)
        {
            return typeof(HtmlElement).IsAssignableFrom(instance.GetType());
        }

        public static bool IsHtmlElementList(FieldInfo field)
        {
            return IsHtmlElementList(field.FieldType);
        }

        public static bool IsHtmlElementList(Type type)
        {
            if (!IsParametrizedGenericList(type))
            {
                return false;
            }
            Type collectionParameterType = GetGenericParameterType(type);
            return IsHtmlElement(collectionParameterType);
        }

        private static bool IsParametrizedGenericList(Type type)
        {
            return IsGenericList(type) && !HasUndefinedGenericParameters(type);
        }

        private static bool IsGenericList(Type type)
        {
            return type.IsGenericType && typeof(IList<>) == type.GetGenericTypeDefinition();
        }

        private static bool HasUndefinedGenericParameters(Type type)
        {
            return type.ContainsGenericParameters;
        }

        public static Type GetGenericParameterType(FieldInfo Field)
        {
            return GetGenericParameterType(Field.FieldType);
        }

        private static Type GetGenericParameterType(Type type)
        {
            if (HasUndefinedGenericParameters(type))
            {
                return null;
            }
            return type.GenericTypeArguments[0];
        }

        public static string GetElementName(FieldInfo field)
        {
            NameAttribute name = field.GetCustomAttribute<NameAttribute>(false);
            if (name != null)
            {
                return name.Name;
            }
            return GetElementName(field.FieldType);
        }

        public static string GetElementName(Type type)
        {
            NameAttribute name = type.GetCustomAttribute<NameAttribute>(false);
            if (name != null)
            {
                return name.Name;
            }
            return SplitCamelCase(type.Name);
        }

        private static String SplitCamelCase(string camel)
        {
            Regex regex = new Regex(string.Format("{0}|{1}|{2}", "(?<=[A-Z])(?=[A-Z][a-z])", "(?<=[^A-Z])(?=[A-Z])", "(?<=[A-Za-z])(?=[^A-Za-z])"));
            string separateWords = regex.Replace(camel, " ");
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(separateWords);
        }

    }
}
