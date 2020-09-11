using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FileConverter.Application;
using FileConverter.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Console
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider;
        private static IConfigurationRoot? _configuration;

        private static void Main()
        {
            RegisterServices();
            var scope = _serviceProvider.CreateScope();

            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();

            DisposeServices();
        }


        private static void RegisterServices()
        {
            // Build configuration
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();


            var services = new ServiceCollection();
            services.AddSingleton(_configuration);

            services.AddPersistenceService(_configuration.GetSection("Resources")["FileLocation"]);
            services.AddApplicationService();

            services.AddScoped<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            _serviceProvider?.Dispose();
        }
    }
}