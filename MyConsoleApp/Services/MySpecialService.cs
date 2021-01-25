using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyConsoleApp.ValueObjects;

namespace MyConsoleApp.Services
{
    public class MySpecialService
    {
        private ImportantSettings AppSettings { get; }
        private readonly ILogger<MySpecialService> _logger;

        public MySpecialService(ILogger<MySpecialService> logger, IOptions<ImportantSettings> appSettings)
        {
            _logger = logger;
            AppSettings = appSettings.Value;
            
            _logger.LogInformation("Service created with out dir[{OutputDirectory}] and important number[{SomeImportantNumber}]", 
                AppSettings.OutputDirectory, 
                AppSettings.SomeImportantNumber);
        }

        public void JustDoIt()
        {
            _logger.LogInformation("Doing all the things");
        }
    }
}