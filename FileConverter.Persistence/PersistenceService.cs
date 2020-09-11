using System.Diagnostics.CodeAnalysis;
using FileConverter.Application.Interfaces.Persistence;
using FileConverter.Persistence.Repository.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Persistence
{
    [ExcludeFromCodeCoverage]
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, string filename)
        {
            services.AddScoped<ICsvFileRepository>(s => new CsvFileRepository(filename));

            return services;
        }
    }
}