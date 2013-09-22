using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Elements
{
    public class FileInput : TypifiedElement
    {
        private const string AssemblyScheme = "assembly";

        public FileInput(IWebElement element)
            : base(element)
        {
        }

        /// <summary>
        /// Pointing input field to a file.
        /// Use file URI scheme or absolute path for separate files: file://host/path; eg. file://localhost/c:/WINDOWS/clock.avi
        /// Use Spring.Net notation for embedded resources: assembly://<AssemblyName>/<NameSpace>/<ResourceName>; eg. assembly://HtmlElements-DotNet/Yandex.HtmlElements/TestResource.txt
        /// For embedded resources the resource will be unpacked to a temp dir.
        /// </summary>
        /// <param name="fileName"></param>
        public void SetFileToUpload(string fileName)
        {
            // Proxy can't be used to check the element class, so find real WebElement
            IWebElement fileInputElement = GetNotProxiedInputElement();
            // Set local file detector in case of remote driver usage
            if (HtmlElementUtils.IsOnRemoteWebDriver(fileInputElement))
            {
                SetLocalFileDetector((RemoteWebElement)fileInputElement);
            }

            string filePath = GetFilePath(fileName);
            fileInputElement.SendKeys(filePath);
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }

        private IWebElement GetNotProxiedInputElement()
        {
            // Cannot get something from WebElementProxy class since it's 'internal'
            return WrappedElement.FindElement(By.XPath("."));
        }

        private void SetLocalFileDetector(RemoteWebElement element)
        {
            ((RemoteWebDriver)element.WrappedDriver).FileDetector = new LocalFileDetector();
        }

        private string GetFilePath(string path)
        {
            Uri fileUri = null;
            try
            {
                fileUri = new Uri(path);
                if (fileUri.Scheme == AssemblyScheme)
                {
                    return HtmlElementUtils.ExtractResource(fileUri);
                }
            }
            catch
            { }
            return GetPathForSystemFile(path);
        }

        private string GetPathForSystemFile(string path)
        {
            return Path.GetFullPath(path);
        }


    }
}
