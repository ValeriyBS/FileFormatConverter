using System;
using FileConverter.Application.ConvertStrategy.Abstract;

namespace FileConverter.Application.ConvertStrategy.Services
{
    public enum ConverterType
    {
        CsvToXml,
        CsvToJson
    }

    public class ConverterFactory : IConverterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ConverterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConverter GetConverter(ConverterType type)
        {
            return type switch
            {
                ConverterType.CsvToXml => (IConverter) _serviceProvider.GetService(typeof(CsvToXmlConverter)),
                ConverterType.CsvToJson => (IConverter) _serviceProvider.GetService(typeof(CsvToJsonConverter)),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}