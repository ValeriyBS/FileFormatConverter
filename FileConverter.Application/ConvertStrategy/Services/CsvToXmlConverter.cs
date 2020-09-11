using System.Linq;
using System.Xml.Linq;
using FileConverter.Application.ConvertStrategy.Abstract;
using FileConverter.Helper.Extensions;

namespace FileConverter.Application.ConvertStrategy.Services
{
    public class CsvToXmlConverter : IConverter
    {
        public string Convert(IQueryable<string> source)
        {
            var headingRow = source
                .First();

            var xEl = new XElement("Root");

            foreach (var row in source
                .Skip(1)
                .Select((value, index)
                    => new {value, index}))
            {
                var xml = new XElement($"Line{row.index + 1}");

                xml.CsvRowToXml(headingRow, row.value);

                xEl.Add(xml);
            }

            return xEl.ToString();
        }
    }
}