using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MyConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyConsoleApp.ValueObjects;
using Microsoft.Extensions.Options;
using Serilog;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup DependencyInjection using the basic MS ServiceCollection
            var serviceCollection = new ServiceCollection();
            
            // We'll add all of our services via this method
            ConfigureServices(serviceCollection);


            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            //do the actual work here
            var mySpecialService = serviceProvider.GetService<MySpecialService>();
            mySpecialService.JustDoIt();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Read the appsettings.json configuration file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<ImportantSettings>(configuration.GetSection("MyAppSettings"));
            
            // Setup Serilog logging
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.RollingFile("MyConsoleApp.log")
                .CreateLogger();

            serviceCollection.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(serilogLogger, true);
            });
            
            // Add all of your application services here
            serviceCollection.AddScoped<MySpecialService>();
        }
    }
}