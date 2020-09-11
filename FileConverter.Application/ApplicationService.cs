using System.Diagnostics.CodeAnalysis;
using FileConverter.Application.ConvertStrategy.Abstract;
using FileConverter.Application.ConvertStrategy.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IConverterFactory, ConverterFactory>();

            services.AddScoped<CsvToXmlConverter>()
                .AddScoped<IConverter, CsvToXmlConverter>(s => s.GetService<CsvToXmlConverter>());

            services.AddScoped<CsvToJsonConverter>()
                .AddScoped<IConverter, CsvToJsonConverter>(s => s.GetService<CsvToJsonConverter>());

            return services;
        }
    }
}