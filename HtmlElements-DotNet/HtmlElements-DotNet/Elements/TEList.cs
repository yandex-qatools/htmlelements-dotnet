using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex.HtmlElements.Utils;

namespace Yandex.HtmlElements.Elements
{
    public class TEList<T> : IList<T>, INamed
        where T : TypifiedElement
    {
        private readonly IList<IWebElement> wrappedElements;
        private string name;

        public TEList(IList<IWebElement> elements)
        {
            this.wrappedElements = elements;
        }

        public IList<IWebElement> WrappedElements
        {
            get { return wrappedElements; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int IndexOf(T item)
        {
            return ToTypifiedElements<T>().IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            throw new InvalidOperationException("Read Only Object");
        }

        public void RemoveAt(int index)
        {
            throw new InvalidOperationException("Read Only Object");
        }

        public T this[int index]
        {
            get
            {
                return ToTypifiedElements<T>()[index];
            }
            set
            {
                throw new InvalidOperationException("Read Only Object");
            }
        }

        public void Add(T item)
        {
            throw new InvalidOperationException("Read Only Object");
        }

        public void Clear()
        {
            throw new InvalidOperationException("Read Only Object");
        }

        public bool Contains(T item)
        {
            return ToTypifiedElements<T>().Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ToTypifiedElements<T>().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ToTypifiedElements<T>().Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            throw new InvalidOperationException("Read Only Object");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ToTypifiedElements<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ToTypifiedElements<T>().GetEnumerator();
        }

        private IList<T> ToTypifiedElements<T>() where T : TypifiedElement
        {
            IList<T> typifiedElements = new List<T>();
            IList<IWebElement> elements = WrappedElements;
            Type typifiedElementType = typeof(T);
            int elementNumber = 0;
            foreach (IWebElement element in elements)
            {
                T typifiedElement = (T)Convert.ChangeType(HtmlElementUtils.newInstance<TypifiedElement>(typifiedElementType, element), typifiedElementType);
                string typifiedElementName = string.Format("{0} {1}", name, elementNumber);
                typifiedElement.Name = typifiedElementName;
                typifiedElements.Add(typifiedElement);
                elementNumber++;
            }
            return typifiedElements;
        }
    }
}
