using Microsoft.Extensions.Logging;

namespace Prueba.Infraestructure.Utils
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly LogLevel _minLevel;
        private static readonly object _lock = new();

        public FileLogger(string filePath, LogLevel minLevel)
        {
            _filePath = "C:/Logs/test-log.txt";
            _minLevel = minLevel;
        }

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                                Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {formatter(state, exception)}";
            if (exception != null)
            {
                message += Environment.NewLine + exception;
            }

            lock (_lock)
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.AppendAllText(_filePath, message + Environment.NewLine);
            }
        }
    }
}