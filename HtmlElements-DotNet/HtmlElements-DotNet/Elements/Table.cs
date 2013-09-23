using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Yandex.HtmlElements.Exceptions;

namespace Yandex.HtmlElements.Elements
{
    public class Table : TypifiedElement
    {
        public Table(IWebElement element)
            : base(element)
        { }

        public List<IWebElement> GetHeadings()
        {
            return WrappedElement.FindElements(By.XPath(".//th")).ToList();
        }

        public List<string> GetHeadingsAsString()
        {
            return GetHeadings().ConvertAll(new Converter<IWebElement, string>(element => GetText(element)));
        }

        public List<List<IWebElement>> GetRows()
        {
            List<List<IWebElement>> rows = new List<List<IWebElement>>();
            List<IWebElement> rowElements = WrappedElement.FindElements(By.XPath(".//tr")).ToList();
            foreach (IWebElement rowElement in rowElements)
            {
                rows.Add(rowElement.FindElements(By.XPath(".//td")).ToList());
            }
            return rows;
        }

        public List<List<string>> GetRowsAsString()
        {
            return GetRows().ConvertAll(new Converter<List<IWebElement>, List<string>>(row => row.ConvertAll(new Converter<IWebElement, string>(element => GetText(element)))));
        }

        public List<List<IWebElement>> GetColumns()
        {
            List<List<IWebElement>> columns = new List<List<IWebElement>>();
            List<List<IWebElement>> rows = GetRows();

            if (rows.Count <= 0)
            {
                return columns;
            }

            int columnsNumber = rows[0].Count;
            for (int i = 0; i < columnsNumber; i++)
            {
                List<IWebElement> column = new List<IWebElement>();
                foreach (List<IWebElement> row in rows)
                {
                    column.Add(row[i]);
                }
                columns.Add(column);
            }

            return columns;
        }

        public List<List<string>> GetColumnsAsString()
        {
            return GetColumns().ConvertAll(new Converter<List<IWebElement>, List<string>>(row => row.ConvertAll(new Converter<IWebElement, string>(element => GetText(element)))));
        }

        public List<IWebElement> this[int index]
        {
            get
            {
                return GetRows()[index];
            }
        }

        public List<IDictionary<string, IWebElement>> GetRowsMappedToHeadings()
        {
            return GetRowsMappedToHeadings(GetHeadingsAsString());
        }

        public List<IDictionary<string, IWebElement>> GetRowsMappedToHeadings(List<String> headings)
        {
            List<IDictionary<string, IWebElement>> rowsMappedToHeadings = new List<IDictionary<string, IWebElement>>();
            List<List<IWebElement>> rows = GetRows();

            if (rows.Count <= 0)
            {
                return rowsMappedToHeadings;
            }

            foreach (List<IWebElement> row in rows)
            {
                if (row.Count != headings.Count)
                {
                    throw new HtmlElementsException("Headings count is not equal to number of cells in row");
                }

                IDictionary<string, IWebElement> rowToHeadingsMap = new Dictionary<string, IWebElement>();
                int cellNumber = 0;
                foreach (string heading in headings)
                {
                    rowToHeadingsMap[heading] = row[cellNumber];
                    cellNumber++;
                }
                rowsMappedToHeadings.Add(rowToHeadingsMap);
            }
            return rowsMappedToHeadings;
        }

        public List<IDictionary<string, string>> GetRowsAsStringMappedToHeadings()
        {
            return GetRowsAsStringMappedToHeadings(GetHeadingsAsString());
        }

        public List<IDictionary<string, string>> GetRowsAsStringMappedToHeadings(List<string> headings)
        {
            Func<IDictionary<string, IWebElement>, IDictionary<string, string>> dictionaryConvert = delegate(IDictionary<string, IWebElement> from)
            {
                IDictionary<string, string> result = new Dictionary<string, string>();
                foreach (KeyValuePair<string, IWebElement> pair in from)
                {
                    result[pair.Key] = GetText(pair.Value);
                }
                return result;
            };
            return GetRowsMappedToHeadings(headings).ConvertAll(new Converter<IDictionary<string, IWebElement>, IDictionary<string, string>>(row => dictionaryConvert(row)));
        }

        private string GetText(IWebElement element)
        {
            return element.Text;
        }
    }
}
