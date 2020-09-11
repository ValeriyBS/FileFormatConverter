using System;
using System.Diagnostics.CodeAnalysis;
using FileConverter.Application.ConvertStrategy.Abstract;
using FileConverter.Application.ConvertStrategy.Services;
using FileConverter.Application.Interfaces.Persistence;
using static System.Console;

namespace FileConverter.Console
{
    [ExcludeFromCodeCoverage]
    public class ConsoleApplication
    {
        private readonly IConverterFactory _converterFactory;
        private readonly ICsvFileRepository _repository;

        public ConsoleApplication(ICsvFileRepository repository,
            IConverterFactory converterFactory)
        {
            _repository = repository;
            _converterFactory = converterFactory;
        }

        public void Run()
        {
            var data = _repository.GetAllRows();

            WriteLine("Csv raw data:");
            foreach (var line in data) WriteLine(line);
            WriteLine();
            WriteLine("Please choose conversion type:");
            foreach (var type in Enum.GetValues(typeof(ConverterType))) WriteLine($"{(int) type!}:{type}");
            WriteLine("Any other key to Exit!");

            ConsoleKeyInfo ch;
            do
            {
                ch = ReadKey();


                var converterService =
                    // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
                    ch.Key switch
                    {
                        ConsoleKey.D0 => _converterFactory.GetConverter(ConverterType.CsvToXml),
                        ConsoleKey.D1 => _converterFactory.GetConverter(ConverterType.CsvToJson),
                        _ => null
                    };


                if (converterService != null)
                    WriteLine(
                        $"{Environment.NewLine}Conversion result is: {Environment.NewLine}{converterService.Convert(data)}");
            } while (ch.Key == ConsoleKey.D0 || ch.Key == ConsoleKey.D1);
        }
    }
}