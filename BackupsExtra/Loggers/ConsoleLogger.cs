using Serilog;
using Serilog.Core;

namespace BackupsExtra
{
    public class ConsoleLogger : ILoggerService
    {
        private Logger _logger;
        public ConsoleLogger(bool logTimeCodeOn)
        {
            if (logTimeCodeOn)
            {
                _logger = new LoggerConfiguration().WriteTo.Console(outputTemplate: "{Timestamp:dd.MM.yy HH:mm:ss} {Message}{NewLine}{Exception}").CreateLogger();
            }
            else
            {
                _logger = new LoggerConfiguration().WriteTo.Console(outputTemplate: "{Message}{NewLine}").CreateLogger();
            }
        }

        public void LoggerOutput(string outPutMessage)
        {
            _logger.Information(outPutMessage);
        }
    }
}