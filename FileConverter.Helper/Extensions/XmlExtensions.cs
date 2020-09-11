using System.Linq;
using System.Xml.Linq;

namespace FileConverter.Helper.Extensions
{
    public static class XmlExtensions
    {
        public static XElement CsvRowToXml(this XElement xml, string csvHeader, string csvRow)
        {
            var columnNames = csvHeader.Trim().Split(",");

            foreach (var column in csvRow
                .Trim()
                .Split(",")
                .Select((value, index) => new {value, index}))

            {
                var columnNameSplit = columnNames[column.index]
                    .Trim()
                    .Split("_");

                if (columnNameSplit.Length > 1)
                {
                    if (xml.Element(columnNameSplit[0]) is null)
                        xml.Add(new XElement(columnNameSplit[0], new XElement(columnNameSplit[1].Trim(), column.value.Trim())));
                    else
                        xml.Element(columnNameSplit[0])?.Add(new XElement(columnNameSplit[1].Trim(), column.value.Trim()));
                }
                else
                {
                    xml.Add(new XElement(columnNames[column.index].Trim(), column.value.Trim()));
                }
            }

            return xml;
        }
    }
}