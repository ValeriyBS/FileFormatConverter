using System.Linq;
using FileConverter.Application.ConvertStrategy.Abstract;
using Newtonsoft.Json.Linq;

namespace FileConverter.Application.ConvertStrategy.Services
{
    public class CsvToJsonConverter : IConverter
    {
        public string Convert(IQueryable<string> source)
        {
            var header = source.First().Trim();
            var rows = source.Skip(1);

            var result = new JArray();
            foreach (var row in rows) result.Add(CsvRowToJson(header, row));


            return result.ToString();
        }


        private static JObject CsvRowToJson(string csvHeader, string csvRow)
        {
            var columnNames = csvHeader.Trim().Split(",");

            var result = new JObject();

            foreach (var columnValue in csvRow
                .Trim()
                .Split(",")
                .Select((value, index) => new {value, index}))
            {
                var columnNameSplit = columnNames[columnValue.index].Split("_");

                if (columnNameSplit.Length > 1)
                {
                    if (result[columnNameSplit[0]] is null)
                        result.Add(columnNameSplit[0], new JObject {{columnNameSplit[1].Trim(), columnValue.value.Trim()}});
                    else
                    {
                        var addOn = (JObject) result[columnNameSplit[0]]!;
                        addOn.Add(new JProperty(columnNameSplit[1].Trim(), columnValue.value.Trim()));
                    }
                }
                else
                {
                    result.Add(new JProperty(columnNames[columnValue.index].Trim(), columnValue.value.Trim()));
                }
            }

            return result;
        }
    }
}