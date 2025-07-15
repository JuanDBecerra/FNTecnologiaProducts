using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Prueba.Infraestructure.Utils
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;
        private readonly LogLevel _minLevel;

        public FileLoggerProvider(IConfiguration config)
        {
            var section = config.GetSection("Logging:File");
            _filePath = "C:/Logs/test-log.txt";

            _minLevel = LogLevel.Information;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath, _minLevel);
        }

        public void Dispose() { }
    }
}


