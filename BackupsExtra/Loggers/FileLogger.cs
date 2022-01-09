using Serilog;
using Serilog.Core;

namespace BackupsExtra
{
    public class FileLogger : ILoggerService
    {
        private Logger _logger;
        public FileLogger(bool logTimeCodeOn)
        {
            if (logTimeCodeOn)
            {
                const string path = "C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Loggs.txt";
                _logger = new LoggerConfiguration().WriteTo.File(path, outputTemplate: "{Timestamp:dd.MM.yy HH:mm:ss} {Message}{NewLine}{Exception}").CreateLogger();
            }
            else
            {
                const string path = "C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Loggs.txt";
                _logger = new LoggerConfiguration().WriteTo.File(path, outputTemplate: "{Message}{NewLine}").CreateLogger();
            }
        }

        public void LoggerOutput(string outPutMessage)
        {
            _logger.Information(outPutMessage);
        }
    }
}